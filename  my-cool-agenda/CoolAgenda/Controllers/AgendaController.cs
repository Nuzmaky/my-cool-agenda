﻿using CoolAgenda.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.Controllers
{
    public class AgendaController : Controller
    {
        //
        // GET: /Agenda/

        [FiltroAutenticacao]
        public ActionResult Index()
        {
            return View();
        }

    }
}
