using CoolAgenda.Models;
using CoolAgenda.Controllers.Utilidades;
using CoolAgenda.ViewModels.Compromisso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolAgenda.Filters;

namespace CoolAgenda.Controllers
{
    [FiltroAutenticacao("U")]
    public class CompromissoController : Controller
    {
        private ICompromissoService compromissoService;
        private ICompromissoUsuarioService compromissoUsuarioService;
        private IGrupoUsuarioService grupoUsuarioService;
        private IContatoService contatoService;
        private ICompromissoContatoService compromissoContatoService;

        public CompromissoController()
        {
            compromissoService = new CompromissoService();
            compromissoUsuarioService = new CompromissoUsuarioService();
            grupoUsuarioService = new GrupoUsuarioService();
            contatoService = new ContatoService();
            compromissoContatoService = new CompromissoContatoService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Cadastrar()
        {
            /**
             * View parcial que só pode receber ser exibida sub-requisição,
             * ou seja, essa action só poderá ser solicitada como parte de outra solicitação
             */
            Usuario pUsuario = Session["Usuario"] as Usuario;
            int idUser = pUsuario.IdUsuario;

            CadastrarVM vm = CadastrarVMNovo();
            vm.ListarGrupo = grupoUsuarioService.ComboListarGruposUsuario(idUser);

            return PartialView(vm);
        }

        public ActionResult Editar(int? id)
        {
            EditarVM vm;

            vm = CadastrarVMEdicao(id.Value);
            if (vm == null)
                return new HttpNotFoundResult();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Editar(EditarVM vm)
        {
            int idCompromisso = vm.IdCompromisso;

            Usuario pUsuario = Session["Usuario"] as Usuario;
            int idUser = pUsuario.IdUsuario;

            if (ModelState.IsValid)
            {
                Compromisso reg = ConverterFormVMEdicao(vm);
                CompromissoUsuario cUser = new CompromissoUsuario();
                cUser.IdUsuario = idUser;
                cUser.IdCompromisso = vm.IdCompromisso;

                List<Validacao> erros = compromissoService.ValidarEntidade(reg);
                List<Validacao> erros2 = compromissoUsuarioService.ValidaAtualizar(cUser);

                if (erros.Count == 0 && erros2.Count == 0)
                {
                    if (vm.Edicao)
                        compromissoService.Atualizar(reg);

                    return RedirectToAction("Index", "Agenda");
                }
                else
                {
                    ModelState.AddModelErrors(erros);
                    ModelState.AddModelErrors(erros2);
                }
            }

            var usuariosCompromisso = compromissoUsuarioService.ListarUsuariosDoCompromisso(idCompromisso, idUser);
            vm.ListaUsuario = usuariosCompromisso;
            vm.TotalRegistros = usuariosCompromisso.Count;

            var contatoCompromisso = compromissoContatoService.ListarContatoDoCompromisso(idCompromisso);
            vm.ListaContato = contatoCompromisso;
            vm.TotalRegistrosContato = contatoCompromisso.Count;

            CompromissoUsuario registro = compromissoUsuarioService.BuscarPorId(idCompromisso, idUser);
            vm.grupoNome = registro.Grupo.Nome;
            PopularItensCadastrarVMEdicao(vm);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastrarVM vm)
        {
            if (ModelState.IsValid)
            {
                Compromisso reg = ConverterFormVM(vm);
                List<CompromissoUsuario> cUser = ConverterFormVMcUser(vm);
                List<CompromissoContato> cContato = ConverterFormVMcContato(vm);

                List<Validacao> erros = compromissoService.ValidarEntidade(reg);

                if (erros.Count == 0)
                {
                    if (!vm.Edicao)
                        compromissoService.Adicionar(reg, cUser, cContato);

                    return Json(new { redirectTo = Url.Action("Index", "Agenda") });
                }
                else
                {
                    ModelState.AddModelErrors(erros);
                }
            }

            Usuario pUsuario = Session["Usuario"] as Usuario;
            int idUser = pUsuario.IdUsuario;

            vm.ListarGrupo = grupoUsuarioService.ComboListarGruposUsuario(idUser);

            PopularItensCadastrarVM(vm);
            return PartialView(vm);
        }

        public JsonResult PegaUsuarios(string q, int idGrupo)
        {
            Usuario pUsuario = Session["Usuario"] as Usuario;
            int idUser = pUsuario.IdUsuario;

            var eventos = from e in grupoUsuarioService.ListarUsuarioPorGrupo(idGrupo, q, idUser)
                          select new
                          {
                              id = e.IdUsuario,
                              name = e.Usuario.Nome,
                              email = e.Usuario.Email,
                              url = "/Images/default_profile_2_normal.png"
                          };

            var rows = eventos.ToArray();

            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        //Popula o token de contatos
        public JsonResult PegaContatos(string q)
        {
            Usuario pUsuario = Session["Usuario"] as Usuario;
            int idUser = pUsuario.IdUsuario;

            var eventos = from e in contatoService.ListarContatosUsuario(idUser, q)
                          select new
                          {
                              id = e.IdContato,
                              name = e.Nome,
                              email = e.Email,
                              url = "/Images/default_profile_2_normal.png"
                          };

            var rows = eventos.ToArray();

            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Aceitar(int id)
        {
            Usuario pUsuario = Session["Usuario"] as Usuario;
            int idUser = pUsuario.IdUsuario;

            compromissoUsuarioService.Aceitar(id, idUser);
            return Json(new JsonActionResultModel("Compromisso aceito!"));
        }

        public JsonResult Rejeitar(int id)
        {
            Usuario pUsuario = Session["Usuario"] as Usuario;
            int idUser = pUsuario.IdUsuario;

            compromissoUsuarioService.Rejeitar(id, idUser);
            return Json(new JsonActionResultModel("Compromisso negado!"));
        }

        // MÉTODOS A SEREM CHAMADOS

        private CadastrarVM CadastrarVMNovo()
        {
            CadastrarVM vm = new CadastrarVM();
            PopularItensCadastrarVM(vm);
            vm.Edicao = false;
            return vm;
        }

        private void PopularItensCadastrarVM(CadastrarVM vm)
        {
            vm.ListarCores = compromissoService.ListarCores();
        }


        private EditarVM CadastrarVMEdicao(int id)
        {
            Usuario pUsuario = Session["Usuario"] as Usuario;
            int idUser = pUsuario.IdUsuario;

            CompromissoUsuario reg = compromissoUsuarioService.BuscarPorId(id, idUser);

            if (reg != null)
            {
                EditarVM vm = ConverterFormVMEdicao(reg);

                //lista usuarios do compromisso menos o requisitante
                var usuariosCompromisso = compromissoUsuarioService.ListarUsuariosDoCompromisso(id, idUser);
                vm.ListaUsuario = usuariosCompromisso;
                vm.TotalRegistros = usuariosCompromisso.Count;

                //lista contatos do compromisso
                var contatoCompromisso = compromissoContatoService.ListarContatoDoCompromisso(id);
                vm.ListaContato = contatoCompromisso;
                vm.TotalRegistrosContato = contatoCompromisso.Count;
                
                vm.Edicao = true;
                PopularItensCadastrarVMEdicao(vm);
                return vm;
            }

            return null;
        }

        private CadastrarVM ConverterFormVM(CompromissoUsuario reg)
        {
            CadastrarVM vm = new CadastrarVM();

            vm.NomeCompromisso = reg.Compromisso.NomeCompromisso;
            DateTime dataInicial = reg.Compromisso.DataInicial;
            DateTime dataFinal = reg.Compromisso.DataFinal;
            vm.DiaInteiro = reg.Compromisso.DiaInteiro;

            if (dataInicial != null)
            {
                vm.DataInicial = dataInicial.ToString("dd/MM/yyyy HH:mm");
            }
            if (dataFinal != null)
            {
                vm.DataFinal = dataFinal.ToString("dd/MM/yyyy HH:mm");
            }

            vm.Cor = reg.Compromisso.Cor;
            vm.Grupo = reg.IdGrupo;

            return vm;
        }

        private Compromisso ConverterFormVM(CadastrarVM vm)
        {
            Compromisso reg = new Compromisso();
            reg.NomeCompromisso = vm.NomeCompromisso;
            reg.DataInicial = DateTime.Parse(vm.DataInicial);
            reg.DataFinal = DateTime.Parse(vm.DataFinal);
            reg.Cor = vm.Cor;
            reg.DiaInteiro = vm.DiaInteiro;

            return reg;
        }

        private List<CompromissoUsuario> ConverterFormVMcUser(CadastrarVM vm)
        {
            List<CompromissoUsuario> reg = new List<CompromissoUsuario>();

            Usuario pUsuario = Session["Usuario"] as Usuario;
            int idUser = pUsuario.IdUsuario;

            CompromissoUsuario compUser = new CompromissoUsuario();
            compUser.IdUsuario = idUser;
            compUser.IdGrupo = vm.Grupo;
            compUser.Criador = "S";
            compUser.Ativo = "S";
            reg.Add(compUser);


            // Pega Usuarios que vao fazer parte do compromisso
            string usuarios = vm.IdUsuarios;

            if (usuarios != null)
            {
                string[] qtdadeusuarios = usuarios.Split(',');

                for (int i = 0; i < qtdadeusuarios.Length; i++)
                {
                    CompromissoUsuario compUser1 = new CompromissoUsuario();
                    compUser1.IdGrupo = vm.Grupo;
                    compUser1.IdUsuario = Int16.Parse(qtdadeusuarios[i]);
                    compUser1.Criador = "N";
                    compUser1.Ativo = "P";
                    reg.Add(compUser1);
                }
            }

            return reg;
        }

        private List<CompromissoContato> ConverterFormVMcContato(CadastrarVM vm)
        {
            List<CompromissoContato> reg = new List<CompromissoContato>();

            // Pega Usuarios que vao fazer parte do compromisso
            string contatos = vm.IdContatos;

            if (contatos != null)
            {
                string[] qtdadecontatos = contatos.Split(',');

                for (int i = 0; i < qtdadecontatos.Length; i++)
                {
                    CompromissoContato compContato = new CompromissoContato();
                    compContato.IdContato = Int16.Parse(qtdadecontatos[i]);
                    compContato.Aceito = "P";
                    reg.Add(compContato);
                }
            }

            return reg;
        }

        private Compromisso ConverterFormVMEdicao(EditarVM vm)
        {
            Compromisso reg = new Compromisso();
            reg.IdCompromisso = vm.IdCompromisso;
            reg.NomeCompromisso = vm.NomeCompromisso;
            reg.DataInicial = DateTime.Parse(vm.DataInicial);
            reg.DataFinal = DateTime.Parse(vm.DataFinal);
            reg.Cor = vm.Cor;
            reg.DiaInteiro = vm.DiaInteiro;

            return reg;
        }

        private void PopularItensCadastrarVMEdicao(EditarVM vm)
        {
            vm.ListarCores = compromissoService.ListarCores();
        }

        private EditarVM ConverterFormVMEdicao(CompromissoUsuario reg)
        {
            EditarVM vm = new EditarVM();

            vm.IdCompromisso = reg.Compromisso.IdCompromisso;
            vm.NomeCompromisso = reg.Compromisso.NomeCompromisso;
            DateTime dataInicial = reg.Compromisso.DataInicial;
            DateTime dataFinal = reg.Compromisso.DataFinal;
            vm.DiaInteiro = reg.Compromisso.DiaInteiro;

            if (dataInicial != null)
            {
                vm.DataInicial = dataInicial.ToString("dd/MM/yyyy HH:mm");
            }
            if (dataFinal != null)
            {
                vm.DataFinal = dataFinal.ToString("dd/MM/yyyy HH:mm");
            }

            vm.Cor = reg.Compromisso.Cor;
            vm.grupoNome = reg.Grupo.Nome;

            return vm;
        }
    }

}
