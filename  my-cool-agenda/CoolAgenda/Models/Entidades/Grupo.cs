using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class Grupo
    {
        private int idGrupo;
        private string nome;

        // ID GRUPO
        public int IdGrupo
        {
            get { return idGrupo; }
            set
            {
                if (value > 0)
                    idGrupo = value;
            }
        }

        //NOME 
        public string Nome
        {   
            get { return nome; }
            set
            {
                if (value != null && value.Length <= 50)
                    nome = value;
            }

        }
    }
}