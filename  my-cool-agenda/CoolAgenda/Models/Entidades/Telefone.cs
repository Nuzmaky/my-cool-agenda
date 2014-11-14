    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace CoolAgenda.Models
{
    public class Telefone
    {
        private int idTelefone;
        private int idContato; //FK
        private string numeroTelefone;

        // ID
        public int IdTelefone
        {
            get { return idTelefone; }
            set
            {
                if (value > 0)
                    idTelefone = value;
            }
        }

        // ID CONTATO _ FK
        public int IdContato
        {
            get { return idContato; }
            set
            {
                if (value > 0)
                    idContato = value;
            }
        }


        // TELEFONE
        public string NumeroTelefone
        {
            get { return numeroTelefone; }
            set
            {
                if (value != null && value.Length <= 11)
                    numeroTelefone = value;
            }
        }


    }
}