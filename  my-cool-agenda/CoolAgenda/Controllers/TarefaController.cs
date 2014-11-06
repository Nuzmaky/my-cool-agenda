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

        Tarefa tarefa = new Tarefa();
        TarefaDAO tarefaDao = new TarefaDAO();

        public ActionResult Index(Tarefa tarefa)
        {
            return View(tarefa);
        }

        [HttpPost]
        public ActionResult Form(Tarefa tarefa)
        {
           tarefaDao.Insert(tarefa);
           return View(tarefa);
        }
    }
}
