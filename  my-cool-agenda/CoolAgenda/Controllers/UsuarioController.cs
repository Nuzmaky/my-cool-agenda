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
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/

        Usuario usuario = new Usuario(); 
        UsuarioDAO usuarioDAO = new UsuarioDAO();


        [FiltroAutenticacao("A")]
        public ActionResult Index(UsuarioVM userVM)
        {            
            return View(userVM);
        }


        
        [HttpPost]
        public ActionResult Form(Usuario usuario, UsuarioVM usuarioVM)
        {
            // Pega o usuário na sessão
            Usuario usuarioSession = Session["Usuario"] as Usuario;
            int idUsuario = usuarioSession.IdUsuario;

            
            //SE FOR USUARIO, CADASTRO COM NIVEL DE USUARIO            
            if (usuarioVM.Nivel)
                usuario.Nivel = "A";
            else
                usuario.Nivel = "U";

            usuarioDAO.Insert(usuario);
            usuarioVM.ListaUsuario = usuarioDAO.Select();            
            return View(usuarioVM);            
        }

    }
}
