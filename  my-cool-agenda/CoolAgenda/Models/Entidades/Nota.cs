using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class Nota
    {
        private int idNota;
        private int idCompromisso; //FK
        private int idUsuario; // FK
        private string texto;
        private string ativo;

        //composicao de classes
        public Usuario Usuario { get; set; }

        // ID
        public int IdNota
        {
            get { return idNota; }
            set
            {
                if (value > 0)
                    idNota = value;
            }
        }


        //ID COMPROMISSO _ FK
        public int IdCompromisso
        {
            get { return idCompromisso; }
            set
            {
                if (value > 0)
                    idCompromisso = value;
            }
        }
        
        
        // ID USUARIO _ FK
        public int IdUsuario
        {
            get { return idUsuario; }
            set
            {
                if (value > 0)
                    idUsuario = value;
            }          
        }


        // TEXTO
        public string Texto
        {
            get { return texto; }
            set
            {
                if (value != null && value.Length < 500)
                    texto = value;
            }
        }

        public string Ativo
        {
            get { return ativo; }
            set
            {
                  ativo = value;
            }
        }

    }
}