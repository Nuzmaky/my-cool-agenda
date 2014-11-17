using System;
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
        private string cor;
        private string ativo;
        private bool diaInteiro;


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
            set { dataInicial = value; }
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

        //COR
        public string Cor
        {
            get { return cor; }
            set
            {
                if (value != null && value.Length <= 10)
                    cor = value;
            }
        }

        //ATIVO
        public string Ativo
        {
            get { return ativo; }
            set
            {
                if (value != null && value.Length == 1)
                {
                    string vFlagUpper = value.ToUpper();
                    if (vFlagUpper.Equals("S") || vFlagUpper.Equals("N"))
                        ativo = vFlagUpper;
                }
            }

        }

        public bool DiaInteiro
        {
            get { return diaInteiro; }
            set
            {
                diaInteiro = value;
            }

        }

    }
}