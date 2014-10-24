using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CoolAgenda.Models.Entidades
{
    public class Usuario
    {
        [StringLength(100, ErrorMessage = "Insira um usuário com um tamanho máximo de 100 caracteres.")]
        [MinLength(4, ErrorMessage = "Insira um usuário com um tamanho mínimo de 4 caracteres.")]
        [Required(ErrorMessage = "Insira um usuário ou e-mail.")]
        [Display(Name = "Usuário")]
        public string Email { get; set; }

        [StringLength(300, ErrorMessage = "Insira uma senha com um tamanho máximo de 300 caracteres.")]
        [MinLength(4, ErrorMessage = "Insira uma senha com um tamanho mínimo de 4 caracteres.")]
        [Required(ErrorMessage = "Insira uma senha.")]
        public string Senha { get; set; }

    }
}