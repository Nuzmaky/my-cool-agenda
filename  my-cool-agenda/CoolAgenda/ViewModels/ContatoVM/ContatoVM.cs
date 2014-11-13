using CoolAgenda.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.ViewModels
{    
    public class ContatoVM
    {

        public bool Edicao { get; set; }

        [Display(Name = "Código: ")]
        public int IdContato { get; set; }

        [Display(Name = "Código do Usuário: ")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Insira um Nome.")]
        [Display(Name = "Nome: ")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Insira um e-mail.")]
        [Display(Name = "E-mail: ")]
        public string Email { get; set; }

        [Display(Name = "Endereço: ")]
        public string Endereco { get; set; }

        [StringLength(14, MinimumLength = 13, ErrorMessage = "Insira o telefone corretamente.")]
        [Required(ErrorMessage = "Insira um telefone.")]
        [Display(Name = "Telefone *")]
        public string Telefone { get; set; }

        public List<Contato> ListaContato { get; set; }
        public int TotalRegistros { get; set; }
    }
}