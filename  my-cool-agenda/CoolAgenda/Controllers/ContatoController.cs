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
        //
        // GET: /Contato/

        Contato contato = new Contato();
        ContatoDAO contatoDAO = new ContatoDAO();       

        public ActionResult Index(ContatoVM contatoVM)
        {
            return View(contatoVM);
        }
        
        [HttpPost]
        public ActionResult Form(Contato contato, ContatoVM contatoVM)
        {
            // Pega o usuário na sessão
            Usuario usuario = Session["Usuario"] as Usuario;
            int idUsuario = usuario.IdUsuario;

            contato.IdUsuario = idUsuario;

            contatoDAO.Insert(contato);
            contatoVM.ListaContato = contatoDAO.Select(); 
            return View(contatoVM);
        }
    }
}
