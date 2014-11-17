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
        private ICompromissoService compromissoService;

        public AgendaController()
        {
            compromissoService = new CompromissoService();
            grupoUsuarioService = new GrupoUsuarioService();
        }

        [FiltroAutenticacao]
        public ActionResult Index()
        {
            Usuario pUsuario = Session["Usuario"] as Usuario;
            int idUser = pUsuario.IdUsuario;

            var usuarioGrupos = grupoUsuarioService.ListarGruposPessoa(idUser);

            AgendaDadosVM vm = new AgendaDadosVM();
            vm.Lista = usuarioGrupos;
            vm.TotalRegistros = usuarioGrupos.Count;

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

        public JsonResult GetEvents()
        {

            //Get the events
            //You may get from the repository also
            var eventos = from e in compromissoService.Listar()
                          select new
                          {
                              id = e.IdCompromisso,
                              title = e.NomeCompromisso,
                              start = e.DataInicial,
                              end = e.DataFinal,
                              color = e.Cor,
                              allDay = e.DiaInteiro
                          };

            var rows = eventos.ToArray();

            return Json(rows, JsonRequestBehavior.AllowGet);
        }


    }
}
