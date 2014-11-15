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
    public class CompromissoController : Controller
    {
        private ICompromissoService compromissoService;

        public CompromissoController()
        {
            compromissoService = new CompromissoService();
        }

        [FiltroAutenticacao]
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
            return PartialView();
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
                Compromisso reg = new Compromisso();

               // reg.IdCompromisso = vm.IdCompromisso;
                reg.NomeCompromisso = vm.NomeCompromisso;
                reg.DataInicial = DateTime.Parse(vm.DataInicial);
                reg.DataFinal = DateTime.Parse(vm.DataFinal);

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

            return PartialView(vm);
        }

        // MÉTODOS A SEREM CHAMADOS
        /*
        private CadastrarVM CadastrarVMNovo()
        {
            CadastrarVM vm = new CadastrarVM();
            vm.Edicao = false;
            return vm;
        }

        
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
    }

}
