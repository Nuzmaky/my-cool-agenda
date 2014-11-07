﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class Compromisso
    {
        private int idCompromisso;
        private string nomeCompromisso;
        private DateTime dataInicial;
        private DateTime dataFinal;


        // ID 
        public int IdCompromisso
        {
            get { return idCompromisso; }
            set
            {
                if (value > 0)
                    idCompromisso = value;
            }
        }

        //DATA INICIAL
        public DateTime DataInicial
        {
            get { return dataInicial; }
            set { dataFinal = value; }
        }

        // DATA FINAL
        public DateTime DataFinal
        {
            get { return dataFinal; }
            set { dataFinal = value; }
        }

        // NOME COMPROMISSO
        public string NomeCompromisso
        {
            get { return nomeCompromisso; }
            set
            {
                if (value != null && value.Length <= 100)
                    nomeCompromisso = value;
            }
        }
    }
}