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

        public CompromissoController()
        {
            compromissoService = new CompromissoService();
            compromissoUsuarioService = new CompromissoUsuarioService();
            grupoUsuarioService = new GrupoUsuarioService();
            contatoService = new ContatoService();
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

        /*public ActionResult Cadastrar(int? id)
        {
            CadastrarVM vm;
            if (id.HasValue)
            {
              //vm = CadastrarVMEdicao(id.Value);
               // if (vm == null)
                    return new HttpNotFoundResult();
            }
            else
            {
                vm = CadastrarVMNovo();
            }

            return View(vm);
        }*/

        [HttpPost]
        public ActionResult Cadastrar(CadastrarVM vm)
        {
            //vm.Edicao = false;
            if (ModelState.IsValid)
            {
                Compromisso reg = ConverterFormVM(vm);
                List<CompromissoUsuario> cUser = ConverterFormVMcUser(vm);
                //CompromissoUsuario compromissoUser = ConverterFormVMCompromissoUser(vm);

               // reg.IdCompromisso = vm.IdCompromisso;

                List<Validacao> erros = compromissoService.ValidarEntidade(reg);

                if (erros.Count == 0)
                {
                   /* if (vm.Edicao)
                        compromissoService.Atualizar(reg);
                    else */
                        compromissoService.Adicionar(reg, cUser);

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

        // MÉTODOS A SEREM CHAMADOS
        
        private CadastrarVM CadastrarVMNovo()
        {
            CadastrarVM vm = new CadastrarVM();
            PopularItensCadastrarVM(vm);
            return vm;
        }

        private void PopularItensCadastrarVM(CadastrarVM vm)
        {
            vm.ListarCores = compromissoService.ListarCores();
        }

        /*
        private CadastrarVM CadastrarVMEdicao(int id)
        {
            Compromisso reg = compromissoService.BuscarPorId(id);
            CadastrarVM vm = null;
            if (registro != null)
            {
                vm = ConverterFormVM(registro);
                vm.Edicao = true;
                PopulaItensFormVM(vm);
            }
            return vm;
        } */

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

                for (int i = 0; i < qtdadeusuarios.Length; i++ )
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
    }

}
