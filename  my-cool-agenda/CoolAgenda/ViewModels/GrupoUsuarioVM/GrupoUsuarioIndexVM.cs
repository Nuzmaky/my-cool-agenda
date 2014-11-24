using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolAgenda.Models;

namespace CoolAgenda.ViewModels.GrupoUsuarioVM
{
    public class GrupoUsuarioIndexVM
    {
        public List<GrupoUsuario> Lista { get; set; }

        public int TotalRegistros { get; set; }
    }
}