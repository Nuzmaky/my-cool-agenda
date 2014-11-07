using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoolAgenda.ViewModels
{
    public class GrupoVM
    {
        [Display(Name = "Código: ")]
        public int IdGrupo{ get; set; }

        [Required(ErrorMessage = "Insira um Nome.")]
        [Display(Name = "Nome: ")]
        public string Nome { get; set; }

        public List<Grupo> ListaGrupo { get; set;}

    }
}