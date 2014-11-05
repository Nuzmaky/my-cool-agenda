using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoolAgenda.ViewModels
{
    public class AutenticacaoVM
    {
        [StringLength(100, ErrorMessage = "Insira um e-mail com um tamanho máximo de 100 caracteres.")]
        [MinLength(4, ErrorMessage = "Insira um e-mail com um tamanho mínimo de 4 caracteres.")]
        [Required(ErrorMessage = "Insira um e-mail.")]
        [Display(Name = "E-mail:")]
        public string Email { get; set; }

        [StringLength(300, ErrorMessage = "Insira uma senha com um tamanho máximo de 300 caracteres.")]
        [MinLength(4, ErrorMessage = "Insira uma senha com um tamanho mínimo de 4 caracteres.")]
        [Required(ErrorMessage = "Insira uma senha.")]
        public string Senha { get; set; }
    }
}