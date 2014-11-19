using CoolAgenda.Models;
using CoolAgenda.Filters;
using CoolAgenda.ViewModels.TarefaVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolAgenda.Controllers.Utilidades;

namespace CoolAgenda.Controllers
{

    [FiltroAutenticacao]
    public class TarefaController : Controller
    {
        
        ITarefaService tarefaService;
        
        public TarefaController()
        {
            tarefaService = new TarefaService();
        
        }

        [FiltroAutenticacao]
        public ActionResult Index()
        {
            TarefaVM vm = ConstruirIndexVM();
            return View(vm);
        }

        public ActionResult Form(int? id)
        {
            TarefaVM vm;
            if (id.HasValue)
            {
                vm = ConstruirFormVMParaEdicao(id.Value);
                if (vm == null)
                    return new HttpNotFoundResult();
            }
            else
            {
                vm = ConstruirFormVMParaNovo();
            }

            return View(vm);
        }

        [HttpPost]
        public ActionResult Form(TarefaVM vm)
        {
            if (ModelState.IsValid)
            {
                Tarefa tarefa = ConverterFormVM(vm);
                
                var erros = tarefaService.ValidarEntidade(tarefa);
                if (erros.Count == 0)
                {
                    if (vm.Edicao)
                        tarefaService.Atualizar(tarefa);
                    else{
                        Usuario usuario = Session["Usuario"] as Usuario;
                        tarefa.IdUsuario = usuario.IdUsuario;
                        tarefaService.Adicionar(tarefa);
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelErrors(erros);
                }
            }

            return View(vm);
        }

        //OUTROS METODOS

        private TarefaVM ConstruirIndexVM()
        {
            TarefaVM vm = new TarefaVM();

            var registros = tarefaService.Listar();
            vm.ListaTarefa = registros;
            vm.TotalRegistros = registros.Count;
            return vm;
        }

        private TarefaVM ConstruirFormVMParaEdicao(int id)
        {
            Tarefa registro = tarefaService.BuscarPorId(id);
            TarefaVM vm = null; 
            if (registro != null)
            {
                vm = ConverterFormVM(registro);
                vm.Edicao = true;
            }
            return vm;
        }

        private TarefaVM ConverterFormVM(Tarefa reg)
        {
            TarefaVM vm = new TarefaVM();
            vm.IdTarefa = reg.IdTarefa;
            vm.IdUsuario = reg.IdUsuario;
            vm.IdCompromisso = reg.IdCompromisso;
            vm.NomeTarefa = reg.NomeTarefa;
            vm.DescTarefa = reg.DescTarefa;
            vm.DataInicial = reg.DataInicial;
            vm.DataFinal = reg.DataFinal;
            

            return vm;
        }

        private TarefaVM ConstruirFormVMParaNovo()
        {
            TarefaVM vm = new TarefaVM();
            vm.Edicao = false;
            return vm;
        }

        private Tarefa ConverterFormVM(TarefaVM vm)
        {
            Tarefa reg = new Tarefa();
            reg.IdTarefa = vm.IdTarefa;
            reg.IdUsuario = vm.IdUsuario;
            reg.IdCompromisso = vm.IdCompromisso;
            reg.NomeTarefa = vm.NomeTarefa;
            reg.DescTarefa = vm.DescTarefa;
            reg.DataInicial = vm.DataInicial;
            reg.DataFinal = vm.DataFinal;

            return reg;
        }
    }
}
