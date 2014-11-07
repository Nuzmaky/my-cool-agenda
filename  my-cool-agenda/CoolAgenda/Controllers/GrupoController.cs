using CoolAgenda.Models;
using CoolAgenda.Filters;
using CoolAgenda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.Controllers
{
    public class GrupoController : Controller
    {
        //
        // GET: /Grupo/
        Grupo grupo = new Grupo();
        GrupoDAO grupoDAO = new GrupoDAO();

        [FiltroAutenticacao]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Form(Grupo grupo, GrupoVM grupoVM)
        {
            grupoDAO.Insert(grupo);
            grupoVM.ListaGrupo = grupoDAO.Select();
            return View(grupoVM);
        }


    }
}
