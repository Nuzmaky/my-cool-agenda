using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models.Entidades
{
    public class Contato
    {

        private int idContato;
        private string nome;
        private string email;
        private string endereco;



        //ID CONTATO
        public int IdContato
        {
            get { return idContato; }
            set
            {
                if (idContato > null)
                    idContato = value;
            }
        }


        //NOME
        public string Nome
        {
            get { return nome; }
            set
            {
                if (nome != null && value.Length < 100)
                    nome = value;
            }
        }
    }
}