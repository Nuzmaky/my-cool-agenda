using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolAgenda.Models;

namespace CoolAgenda.ViewModels.AgendaVM
{
    public class AgendaDadosVM
    {
        public List<GrupoUsuario> Lista { get; set; }

        public int TotalRegistros { get; set; }

        public List<Tarefa> ListaTarefa { get; set; }

        public int TotalRegistrosTarefa { get; set; }
    }
}