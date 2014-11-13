﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class Tarefa
    {
        public bool Edicao { get; set; }
        public int IdTarefa { get; set; }
        public int IdCompromisso { get; set; }
        public int IdUsuario { get; set; }
        public string NomeTarefa { get; set; }
        public string DescTarefa { get; set; }
        public string DataInicial { get; set; }
        public string DataFinal { get; set; }
        public string Ativo { get; set; }

    }

}
