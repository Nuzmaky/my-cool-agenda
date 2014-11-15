using CoolAgenda.Filters;
using CoolAgenda.Models;
using CoolAgenda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace CoolAgenda.Controllers
{
    
    public class AgendaController : Controller
    {
        //
        // GET: /Agenda/

        [FiltroAutenticacao("U")]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents(DateTime start, DateTime end)
        {
            var fromDate = ToUnixTimespan(start);
            var toDate = ToUnixTimespan(end);

            //Get the events
            //You may get from the repository also
            var eventList = GetEvents();

            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        private List<Compromisso> GetEvents()
        {
            List<Compromisso> eventList = new List<Compromisso>();

            Compromisso newEvent = new Compromisso
            {
                IdCompromisso = 10,
                NomeCompromisso = "Event 1",
                DataInicial = DateTime.Now.AddDays(0),
                DataFinal = DateTime.Now.AddDays(1),
            };


            eventList.Add(newEvent);

            newEvent = new Compromisso
            {
                IdCompromisso = 11,
                NomeCompromisso = "Event 3",
                DataInicial = DateTime.Now.AddDays(2),
                DataFinal = DateTime.Now.AddDays(3),
            };

            eventList.Add(newEvent);

            return eventList;
        }

        private long ToUnixTimespan(DateTime date)
        {
            TimeSpan tspan = date.ToUniversalTime().Subtract(
            new DateTime(1970, 1, 1, 0, 0, 0));

            return (long)Math.Truncate(tspan.TotalSeconds);
        }
    }
}
