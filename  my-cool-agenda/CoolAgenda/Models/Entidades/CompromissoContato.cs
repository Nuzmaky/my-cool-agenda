using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class CompromissoContato
    {
        private string aceito;

        //chave estrangeira
        private int idContato;
        private int idCompromisso;

        //composicao de classes
        public Contato Contato { get; set; }
        public Compromisso Compromisso { get; set; }

        //NOME 
        public int IdContato
        {
            get { return idContato; }
            set
            {
                if (value > 0)
                    idContato = value;
            }

        }

        //COMPROMISSO 
        public int IdCompromisso
        {
            get { return idCompromisso; }
            set
            {
                if (value > 0)
                    idCompromisso = value;
            }

        }

        public string Aceito
        {
            get { return aceito; }
            set
            {
                if (value != null && value.Length == 1)
                {
                    string vFlagUpper = value.ToUpper();
                    if (vFlagUpper.Equals("S") || vFlagUpper.Equals("N") || vFlagUpper.Equals("P"))
                        aceito = vFlagUpper;
                }
            }

        }
    }
}