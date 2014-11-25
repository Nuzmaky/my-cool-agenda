using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.ViewModels.UserVM
{
    public class MudarNome
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Insira um Nome.")]
        [Display(Name = "Nome: ")]
        public string Nome { get; set; }
    }
}