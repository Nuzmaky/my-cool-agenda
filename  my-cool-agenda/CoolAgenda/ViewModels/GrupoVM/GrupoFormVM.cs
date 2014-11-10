using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CoolAgenda.ViewModels.GrupoVM
{
    public class GrupoFormVM
    {
        public bool Edicao { get; set; }

        [Display(Name = "Código: ")]
        public int IdGrupo { get; set; }

        [Required(ErrorMessage = "Insira um Nome.")]
        [Display(Name = "Nome: *")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Insira um CNPJ.")]
        [CNPJ(true, ErrorMessage = "Insira um CNPJ válido.")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "O CNPJ deve ter 18 caracteres.")]
        [Display(Name = "CNPJ *")]
        public string CNPJ { get; set; }

        [Display(Name = "Ativo")]
        public bool FlagAtivo { get; set; }
    }
}