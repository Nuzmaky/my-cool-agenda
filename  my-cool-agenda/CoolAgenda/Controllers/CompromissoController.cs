using CoolAgenda.Models;
using CoolAgenda.Controllers.Utilidades;
using CoolAgenda.ViewModels.Compromisso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.Controllers
{
    public class CompromissoController : Controller
    {
        private ICompromissoService compromissoService;

        public CompromissoController()
        {
            compromissoService = new CompromissoService();
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
            return PartialView();
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastrarVM vm)
        {
            if (ModelState.IsValid)
            {
                Compromisso reg = new Compromisso();

                reg.IdCompromisso = vm.IdCompromisso;
                reg.NomeCompromisso = vm.NomeCompromisso;
                reg.DataInicial = DateTime.Parse(vm.DataInicial);
                reg.DataFinal = DateTime.Parse(vm.DataFinal);

                List<Validacao> erros = compromissoService.ValidarEntidade(reg);

                if (erros.Count == 0)
                {
                    if (vm.Edicao)
                        compromissoService.Atualizar(reg);
                    else
                        compromissoService.Adicionar(reg);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelErrors(erros);
                }
            }

            return View(vm);
        }


    }

}
