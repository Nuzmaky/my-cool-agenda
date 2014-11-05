using CoolAgenda.Models;
using CoolAgenda.Models.Services;
using CoolAgenda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.Controllers
{
    public class HomeController : Controller
    {
        private UsuarioService usuarioService;

        public HomeController()
        {
            usuarioService = new UsuarioService();
        }
       
        public ActionResult Index()
        {
            return View();
        }


        //Autenticação
        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {
            string email = usuario.Email;
            string senha = usuario.Senha;

            var erros = usuarioService.ValidaUsuario(email, senha);
            if (erros.Count == 0)
            {
                Usuario u = usuarioService.AutenticaUsuario(email, senha);

                // Adiciona o usuário na sessão
                Session["Usuario"] = u;

                // Redireciona para o controller adequado de acordo com o nível do usuário
                string nivel = u.Nivel;

                // Redireciona para as actions padrões de acordo com o nivel do usuário
                if (Autenticacao.VerificaAdm(nivel))
                    return RedirectToAction("Index", "Usuario");
                else
                    return RedirectToAction("Index", "Agenda");
            }
            
            return View();
        }

                


        
    }
}
