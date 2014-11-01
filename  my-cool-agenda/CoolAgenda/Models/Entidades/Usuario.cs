using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models.Entidades
{
    public class Usuario
    {
        private int idUsuario;
        private string email;
        private string senha;
        private string nome;

        //Lista
        public List<Usuario> listaUsuario = new List<Usuario>();

        // USUARIO
        public int IdUsuario
        {
            get { return idUsuario; }
            set
            {
                //if (idUsuario > 0)
                    idUsuario = value;
            }
        }


        // EMAIL
        public string Email
        {
            get { return email; }
            set 
            {
                //if (email != null && value.Length < 50)
                    email = value;
            }
        }


        // SENHA
        public string Senha
        {
            get { return senha; }
            set
            {
                if (value.Length < 50)
                    senha = value;
            }

        }

        // NOME
        public string Nome
        {
            get { return nome; }
            set
            {
                //if (nome != null && value.Length < 100)
                    nome = value;
            }
        }

    }
}