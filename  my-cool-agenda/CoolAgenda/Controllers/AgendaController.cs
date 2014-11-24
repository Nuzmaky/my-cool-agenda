using CoolAgenda.Filters;
using CoolAgenda.Models;
using CoolAgenda.ViewModels.AgendaVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace CoolAgenda.Controllers
{
    [FiltroAutenticacao("U")]
    public class AgendaController : Controller
    {
        //
        // GET: /Agenda/
        private IGrupoUsuarioService grupoUsuarioService;
        private ICompromissoUsuarioService compromissoUsuarioService;
        private ITarefaService tarefaService;

        public AgendaController()
        {
            compromissoUsuarioService = new CompromissoUsuarioService();
            grupoUsuarioService = new GrupoUsuarioService();
            tarefaService = new TarefaService();
        }

        [FiltroAutenticacao]
        public ActionResult Index()
        {
            Usuario pUsuario = Session["Usuario"] as Usuario;
            int idUser = pUsuario.IdUsuario;

            var usuarioGrupos = grupoUsuarioService.ListarGruposPessoa(idUser);
            var tarefas = tarefaService.ListarId(idUser);

            AgendaDadosVM vm = new AgendaDadosVM();
            vm.Lista = usuarioGrupos;
            vm.TotalRegistros = usuarioGrupos.Count;
            vm.ListaTarefa = tarefas;
            vm.TotalRegistrosTarefa = tarefas.Count;

            if (vm.TotalRegistros == 0)
            {
                return RedirectToAction("SemGrupos",
                    new { titulo = "Erro", mensagem = "Seu cadastro não possue vínculos a nenhum grupo, ou o Grupo está Inativo. Em caso de dúvidas entre em contato com o Administrador." });
            }
            else
            {
                return View(vm);
            }
        }

        public ActionResult SemGrupos(string titulo, string mensagem)
        {
            ViewBag.Titulo = titulo;
            ViewBag.Mensagem = mensagem;
            return View();
        }

        //public JsonResult GetEvents()
        //{
        //    //Pega os eventos
        //    Usuario pUsuario = Session["Usuario"] as Usuario;
        //    int idUser = pUsuario.IdUsuario;

        //    var eventos = from e in compromissoUsuarioService.Listar(idUser)
        //                  select new
        //                  {
        //                      id = e.IdCompromisso,
        //                      title = e.Compromisso.NomeCompromisso,
        //                      start = e.Compromisso.DataInicial,
        //                      end = e.Compromisso.DataFinal,
        //                      color = e.Compromisso.Cor,
        //                      allDay = e.Compromisso.DiaInteiro
        //                  };

        //    var rows = eventos.ToArray();

        //    return Json(rows, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetEvents(int? id)
        {
            //Pega os eventos
            Usuario pUsuario = Session["Usuario"] as Usuario;
            int idUser = pUsuario.IdUsuario;

            if (id.HasValue)
            {
                var eventos = from e in compromissoUsuarioService.ListarPorGrupo(idUser, id.Value)
                              select new
                              {
                                  id = e.IdCompromisso,
                                  title = e.Compromisso.NomeCompromisso,
                                  start = e.Compromisso.DataInicial,
                                  end = e.Compromisso.DataFinal,
                                  color = e.Compromisso.Cor,
                                  allDay = e.Compromisso.DiaInteiro,
                                  grupo = e.Grupo.Nome,
                                  aceito = e.Ativo,
                                  className = e.Ativo + "class",
                                  criador = e.Criador
                              };

                var rows = eventos.ToArray();

                return Json(rows, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var eventos = from e in compromissoUsuarioService.Listar(idUser)
                              select new
                              {
                                  id = e.IdCompromisso,
                                  title = e.Compromisso.NomeCompromisso,
                                  start = e.Compromisso.DataInicial,
                                  end = e.Compromisso.DataFinal,
                                  color = e.Compromisso.Cor,
                                  allDay = e.Compromisso.DiaInteiro,
                                  grupo = e.Grupo.Nome,
                                  aceito = e.Ativo,
                                  className = e.Ativo + "class",
                                  criador = e.Criador
                              };

                var rows = eventos.ToArray();

                return Json(rows, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
