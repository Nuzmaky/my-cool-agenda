using CoolAgenda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.Controllers
{
    public class AutenticacaoController : Controller
    {
        //
        // GET: /Autenticacao/



        public ActionResult Index(AutenticacaoVM vm)
        {
            
            string usuario = vm.Email;
            string senha = vm.Senha;

            if (Session["IdUsuario"] == null)
            {
                Session.Timeout = 10;
                Session.Add("IdUsuario","1");
            }

            return View();
        }

        // Sair
        public ActionResult Sair()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}
