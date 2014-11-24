using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolAgenda.Models;

namespace CoolAgenda.ViewModels.Compromisso
{
    public class EditarVM
    {

        public int IdCompromisso { get; set; }

        public bool Edicao { get; set; }

        public List<CompromissoUsuario> ListaUsuario { get; set; }
        public int TotalRegistros { get; set; }

        public List<CompromissoContato> ListaContato { get; set; }
        public int TotalRegistrosContato { get; set; }

        [Display(Name = "Nome *")]
        [Required(ErrorMessage = "Insira um nome para o Compromisso")]
        [MinLength(3, ErrorMessage = "O campo deve ter no mínimo 3 caracteres")]
        [MaxLength(100, ErrorMessage = "O campo deve ter no máximo 100 caracteres")]
        public string NomeCompromisso { get; set; }

        [Display(Name = "Data Inicial  *")]
        [Required(ErrorMessage = "Insira uma data e horário inicial para o Compromisso")]
        [DisplayFormat(DataFormatString = "dd/mm/yyy hh24:mi:ss")]
        public string DataInicial { get; set; }

        [Display(Name = "Data Final  *")]
        [Required(ErrorMessage = "Insira uma data e horário final para o Compromisso")]
        [DisplayFormat(DataFormatString = "dd/mm/yyy hh24:mi:ss")]
        public string DataFinal { get; set; }

        [Display(Name = "Cor")]
        public string Cor { get; set; }

        [Display(Name = "Dia Inteiro")]
        public bool DiaInteiro { get; set; }

        public List<SelectListItem> ListarCores { get; set; }

        public string grupoNome;
    }
}