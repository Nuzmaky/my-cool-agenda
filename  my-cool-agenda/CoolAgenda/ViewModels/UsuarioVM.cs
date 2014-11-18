using CoolAgenda.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.ViewModels
{
    public class UsuarioVM
    {
        public bool Edicao { get; set; }

        [Display(Name = "Código: ")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Insira um e-mail.")]
        [Display(Name = "E-mail: ")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Insira um Nome.")]
        [Display(Name = "Nome: ")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Insira uma senha.")]
        [Display(Name = "Senha: ")]
        public string Senha { get; set; }

        [Display(Name = "Usuário Administrador. ")]
        public bool Nivel { get; set; }

        [Display(Name = "Ativo: ")]
        public string Ativo { get; set; }

        public List<Usuario> ListaUsuario { get; set; }
        public int TotalRegistros { get; set; }
        public bool clicouAtivacao { get; set; }
    }
}