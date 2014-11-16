using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolAgenda.Models;

namespace CoolAgenda.ViewModels.TarefaVM
{
    public class TarefaVM
    {
        public bool Edicao { get; set; }
        
        public int IdTarefa { get; set; }

        [Display(Name = "Compromisso")]
        public int IdCompromisso { get; set; }

        public int IdUsuario { get; set; }

        [Display(Name = "Nome *")]
        [Required(ErrorMessage = "Insira um nome para a Tarefa")]
        [MinLength(3, ErrorMessage = "O campo deve ter no mínimo 3 caracteres")]
        [MaxLength(100, ErrorMessage = "O campo deve ter no máximo 100 caracteres")]
        public string NomeTarefa { get; set; }

        [Display(Name = "Descrição *")]
        [Required(ErrorMessage = "Insira uma Decrição para a Tarefa")]
        [MinLength(3, ErrorMessage = "O campo deve ter no mínimo 20 caracteres")]
        [MaxLength(100, ErrorMessage = "O campo deve ter no máximo 400 caracteres")]
        public string DescTarefa { get; set; }

        [Display(Name = "Data Inicial  *")]
        [Required(ErrorMessage = "Insira uma data e horário inicial para a Tarefa")]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public string DataInicial { get; set; }

        [Display(Name = "Data Final  *")]
        [Required(ErrorMessage = "Insira uma data e horário final para a Tarefa")]
        [DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public string DataFinal { get; set; }

        public string Ativo { get; set; }

        public List<Tarefa> ListaTarefa { get; set; }
        public int TotalRegistros { get; set; }

    }


}
    