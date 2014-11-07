using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.Controllers
{
    public class ErroController : Controller
    {
        //
        // GET: /Erro/
        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult Forbidden()
        {
            return View();
        }

    }
}
