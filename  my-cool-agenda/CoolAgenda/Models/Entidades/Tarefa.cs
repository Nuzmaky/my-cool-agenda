using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class Tarefa
    {
        public bool Edicao { get; set; }
        public int IdTarefa { get; set; }
        public int? IdCompromisso { get; set; }
        public int IdUsuario { get; set; }
        public int IdGrupo { get; set; }
        public int Criador { get; set; }
        public string NomeTarefa { get; set; }
        public string DescTarefa { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public string Concluida { get; set; }
        public string Ativo { get; set; }

        //composicao de classes
        public Usuario Usuario { get; set; }
        public Usuario UsuarioCriador { get; set; }
        public Grupo Grupo { get; set; }
    }

}
