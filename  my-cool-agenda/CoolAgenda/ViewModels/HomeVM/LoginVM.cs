using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.ViewModels.HomeVM
{
    public class LoginVM
    {
        [EmailAddress]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "O email deve ter no mínimo 8 e no máximo 100 caracteres.")]
        [Required(ErrorMessage = "Insira um email.")]
        [Display(Name = "E-mail *")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "Insira uma senha com um tamanho máximo de 50 caracteres.")]
        [MinLength(4, ErrorMessage = "Insira uma senha com um tamanho mínimo de 4 caracteres.")]
        [Required(ErrorMessage = "Insira uma senha.")]
        public string Senha { get; set; }
    }
}