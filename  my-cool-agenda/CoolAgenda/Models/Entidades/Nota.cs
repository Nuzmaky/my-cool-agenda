using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models.Entidades
{
    public class Nota
    {
        private int idNota;
        private int idCompromisso; //FK
        private int idUsuario; // FK
        private string texto;

        // ID
        public int IdNota
        {
            get { return idNota; }
            set
            {
                if (idNota > 0) idNota = value;
            }
        }


        //ID COMPROMISSO _ FK
        public int IdCompromisso
        {
            get { return idCompromisso; }
            set { idCompromisso = value; }
        }
        
        
        // ID USUARIO _ FK
        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }          
        }


        // TEXTO
        public string Texto
        {
            get { return texto; }
            set
            {
                if (texto != null && value.Length < 500)
                    texto = value;
            }
        }        

    }
}