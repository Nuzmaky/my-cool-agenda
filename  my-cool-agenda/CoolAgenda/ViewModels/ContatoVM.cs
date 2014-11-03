using CoolAgenda.Models.Entidades;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.ViewModels
{
    public class ContatoVM
    {
        [Display(Name = "Código: ")]
        public int IdContato { get; set; }

        [Display(Name = "Código: ")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Insira um Nome.")]
        [Display(Name = "Nome: ")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Insira um e-mail.")]
        [Display(Name = "E-mail: ")]
        public string Email { get; set; }

        [Display(Name = "Endereço: ")]
        public string Endereco { get; set; }

        public List<Contato> ListaContato { get; set; }
    }
}