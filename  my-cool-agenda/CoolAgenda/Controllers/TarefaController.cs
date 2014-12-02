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

    [FiltroAutenticacao("U")]
    public class TarefaController : Controller
    {
        
        ITarefaService tarefaService;
        private IGrupoUsuarioService grupoUsuarioService;
        
        public TarefaController()
        {
            tarefaService = new TarefaService();
            grupoUsuarioService = new GrupoUsuarioService();
        
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
                        tarefa.Criador = usuario.IdUsuario;
                        tarefaService.Adicionar(tarefa);
                        

                        // Mateus - E-mail                        
                        List<Tarefa> ListaUsuario = tarefaService.ListarId(tarefa.IdUsuario);
                        
                        UsuarioService usuarioService = new UsuarioService();
                        
                        // Buscar o Usuário que está na tarefa
                        usuario = usuarioService.BuscarPorId(tarefa.IdUsuario);

                        // Envia E-mail
                        //TarefaService.EnviaEmailTarefa(usuario.Email, usuario.Nome);
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

        public JsonResult Desativar(int id)
        {
            tarefaService.DesativarPorId(id);
            return Json(new JsonActionResultModel("Tarefa excluída!"));
        }

        public JsonResult Concluir(int id)
        {
            tarefaService.ConcluirPorId(id);
            return Json(new JsonActionResultModel("Tarefa finalizada!"));
        }

        //OUTROS METODOS

        private TarefaVM ConstruirIndexVM()
        {
            TarefaVM vm = new TarefaVM();
            Usuario usuario = Session["Usuario"] as Usuario;
            var registros = tarefaService.Listar(usuario.IdUsuario);
            var registrosReq = tarefaService.ListarReq(usuario.IdUsuario);

            vm.ListaTarefa = registros;
            vm.TotalRegistros = registros.Count;

            vm.ListaTarefaReq = registrosReq;
            vm.TotalRegistrosReq = registrosReq.Count;
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
            vm.Criador = reg.Criador;
            vm.DescTarefa = reg.DescTarefa;
            vm.DataInicial = reg.DataInicial;
            vm.DataFinal = reg.DataFinal;
            if (reg.Concluida == "S") { vm.Concluida = true; }       

            return vm;
        }

        private TarefaVM ConstruirFormVMParaNovo()
        {
            TarefaVM vm = new TarefaVM();
            Usuario pUsuario = Session["Usuario"] as Usuario;
            int idUser = pUsuario.IdUsuario;
            vm.ListarGrupo = grupoUsuarioService.ComboListarGruposUsuario(idUser);
            vm.Edicao = false;
            return vm;
        }

        private Tarefa ConverterFormVM(TarefaVM vm)
        {
            Tarefa reg = new Tarefa();
            reg.IdTarefa = vm.IdTarefa;
            reg.IdUsuario = vm.IdUsuario;
            reg.IdGrupo = vm.Grupo;
            reg.IdCompromisso = vm.IdCompromisso;
            reg.NomeTarefa = vm.NomeTarefa;
            reg.Criador = vm.Criador;
            reg.DescTarefa = vm.DescTarefa;
            reg.DataInicial = vm.DataInicial;
            reg.DataFinal = vm.DataFinal;
            if (vm.Concluida) { reg.Concluida = "S"; }

            return reg;
        }
    }
}
