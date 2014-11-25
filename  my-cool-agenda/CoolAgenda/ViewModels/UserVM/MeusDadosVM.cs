using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.ViewModels.UserVM
{
    public class MeusDadosVM
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Insira a senha atual.")]
        [Display(Name = "Senha Atual: ")]
        public string SenhaAtual { get; set; }

        [StringLength(300, MinimumLength = 4, ErrorMessage = "A senha deve ter no mínimo 4 e no máximo 30 caracteres.")]
        [Required(ErrorMessage = "Insira a nova senha.")]
        [Display(Name = "Nova Senha: ")]
        public string SenhaNova { get; set; }

        [Display(Name = "Senha confirmação *")]
        [System.ComponentModel.DataAnnotations.Compare("SenhaNova", ErrorMessage = "A senha não corresponde a nova senha.")]
        public string SenhaConfirmacao { get; set; }

    }
}