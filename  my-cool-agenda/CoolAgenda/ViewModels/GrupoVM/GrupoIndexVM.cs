using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CoolAgenda.ViewModels.GrupoVM
{
    public class GrupoIndexVM
    {
        public List<Grupo> ListaGrupo { get; set; }
        public int TotalRegistros { get; set; }
    }
}