using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolAgenda.Models;

namespace CoolAgenda.ViewModels.NotaVM
{
    public class NotaVM
    {
        public bool Edicao { get; set; }

        public int IdNota { get; set; }

        [Display(Name = "Compromisso")]
        public int IdCompromisso { get; set; }

        [Display(Name = "Usuário")]
        public int IdUsuario { get; set; }

        [Display(Name = "Texto *")]
        [Required(ErrorMessage = "Insira um Texto para a Nota")]
        [MinLength(20, ErrorMessage = "O campo deve ter no mínimo 20 caracteres")]
        [MaxLength(400, ErrorMessage = "O campo deve ter no máximo 400 caracteres")]
        public string Texto { get; set; }

        public string Ativo { get; set; }

        public List<Nota> ListaNota { get; set; }
        public int TotalRegistros { get; set; }

    }


}
