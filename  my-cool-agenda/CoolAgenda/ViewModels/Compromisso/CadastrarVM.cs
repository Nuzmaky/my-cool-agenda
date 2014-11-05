using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.ViewModels.Compromisso
{
    public class CadastrarVM
    {
        public bool Edicao { get; set; }

        public int IdCompromisso { get; set; }

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
    }
}