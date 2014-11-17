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

        public CompromissoController()
        {
            compromissoService = new CompromissoService();
            compromissoUsuarioService = new CompromissoUsuarioService();
            grupoUsuarioService = new GrupoUsuarioService();
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
                //CompromissoUsuario compromissoUser = ConverterFormVMCompromissoUser(vm);

               // reg.IdCompromisso = vm.IdCompromisso;

                List<Validacao> erros = compromissoService.ValidarEntidade(reg);

                if (erros.Count == 0)
                {
                   /* if (vm.Edicao)
                        compromissoService.Atualizar(reg);
                    else */
                        compromissoService.Adicionar(reg);

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

        public JsonResult PegaUsuarios(int grupoID)
        {
            //Get the events
            //You may get from the repository also
            var eventos = from e in grupoUsuarioService.ListarUsuarioPorGrupo(grupoID)
                          select new
                          {
                              id = e.IdUsuario,
                              first_name = e.Usuario.Nome,
                              email = e.Usuario.Email,
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

        //private CompromissoUsuario ConverterFormVMCompromissoUser(CadastrarVM vm)
        //{
        //    CompromissoUsuario compromissoUser = new CompromissoUsuario();
        //    compromissoUser.IdUsuario = vm.Nome;
        //    compromissoUser.IdGrupo = vm.Email;
        //    compromissoUser.Criador = "S";
        //    compromissoUser.Nivel = "U";
        //    compromissoUser.Ativo = "S";

        //    return user;
        //}
    }

}
