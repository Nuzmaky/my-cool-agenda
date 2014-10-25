using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models.Entidades
{
    public class Contato
    {

        private int idContato;
        private int idUsuario; // FK
        private string nome;
        private string email;
        private string endereco;


        //ID CONTATO
        public int IdContato
        {
            get { return idContato; }
            set
            {
                if (idContato > 0)
                    idContato = value;
            }
        }


        // ID USUARIO _ FK
        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
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

        //EMAIL
        public string Email
        {
            get { return email; }
            set
            {
                if (email != null && value.Length < 50)
                    email = value;
            }
        }


        //ENDEREÇO
        public string Endereco
        {
            get { return endereco; }
            set
            {
                if (value.Length < 100)
                    endereco = value;
            }
        }


    }
}