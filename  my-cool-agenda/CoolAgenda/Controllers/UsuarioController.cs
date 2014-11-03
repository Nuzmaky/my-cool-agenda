using CoolAgenda.Models.DAOs;
using CoolAgenda.Models.Entidades;
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

        Usuario user = new Usuario(); 
        UsuarioDAO usuarioDAO = new UsuarioDAO();       

        public ActionResult Index(UsuarioVM userVM)
        {            
            return View(userVM);
        }

        [HttpPost]
        public ActionResult Form(Usuario usuario, UsuarioVM userVM)
        {
            usuarioDAO.Insert(usuario);
            userVM.ListaUsuario = usuarioDAO.Select();            
            return View(userVM);
            
        }

    }
}
