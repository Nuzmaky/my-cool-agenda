using CoolAgenda.Filters;
using CoolAgenda.Models;
using CoolAgenda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.Controllers
{
    public class TarefaController : Controller
    {
        [FiltroAutenticacao("U")]
        public ActionResult Index()
        {
            return View();
        }

    }
}
