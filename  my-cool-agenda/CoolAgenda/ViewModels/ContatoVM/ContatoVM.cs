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

        [Display(Name = "Apelido: ")]
        public string Apelido { get; set; }
        
        [Required(ErrorMessage = "Insira um e-mail.")]
        [Display(Name = "E-mail: ")]
        public string Email { get; set; }

        [Display(Name = "Endereço: ")]
        public string Endereco { get; set; }

        [Display(Name = "Data Nascimento  *")]
        [Required(ErrorMessage = "Insira a Data de Nascimento")]
        [DisplayFormat(DataFormatString = "dd/mm/yyy")]
        public DateTime DataNascimento { get; set; }

        [StringLength(15, MinimumLength = 13, ErrorMessage = "Insira o primeiro telefone corretamente.")]
        [Required(ErrorMessage = "Insira a primeira opção de telefone.")]
        [Display(Name = "Telefone *")]
        public string telefoneUm { get; set; }

        [StringLength(15, MinimumLength = 13, ErrorMessage = "Insira o segundo telefone corretamente.")]
        [Display(Name = "Telefone opcional")]
        public string telefoneDois { get; set; }

        [Display(Name = "Ativo. ")]
        public bool Ativo { get; set; }

        [Display(Name = "Mensagem: ")]
        public string CorpoEmail { get; set; }

        public List<Contato> ListaContato { get; set; }
        public List<Telefone> ListaTelefone { get; set; }
        public List<string> Telefones { get; set; }
        public int TotalRegistros { get; set; }
    }
}