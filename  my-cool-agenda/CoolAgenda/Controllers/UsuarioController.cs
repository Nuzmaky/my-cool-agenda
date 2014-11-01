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
        UsuarioDAO usuarioDAO = new UsuarioDAO();
        Usuario user = new Usuario();


        public ActionResult Index(UsuarioFormVM userVM)
        {
            userVM.ListaUsuario = usuarioDAO.Select();
            return View(userVM);
        }

        [HttpPost]
        public ActionResult Form(Usuario usuario)
        {
            usuarioDAO.Insert(usuario);
            
            return View();
            
        }

    }
}
