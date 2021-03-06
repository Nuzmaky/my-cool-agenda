﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace CoolAgenda.Models
{
    public class Usuario
    {
        private int idUsuario;
        private string email;
        private string senha;
        private string nome;
        
        private string nivel;
        private string ativo;

        public bool clicouAtivacao { get; set; }

        //Lista
        public List<Usuario> listaUsuario = new List<Usuario>();

        // USUARIO
        public int IdUsuario
        {
            get { return idUsuario; }
            set
            {
                if (value > 0)
                    idUsuario = value;              
            }
        }


        // EMAIL
        public string Email
        {
            get { return email; }
            set
            {
                if (value != null && value.Length < 100)
                    email = value;
            }
        }


        // SENHA
        public string Senha
        {
            get { return senha; }
            set
            {
                if (value != null && value.Length < 50)
                    senha = value;
            }

        }

        // NOME
        public string Nome
        {
            get { return nome; }
            set
            {
                if (value != null && value.Length < 100)
                    nome = value;
            }
        }

        //Nivel
        public string Nivel
        {
            get { return nivel; }
            set
            {
                if (value != null && value.Length < 2)
                nivel = value;
            }
        }
        public string Ativo
        {
            get { return ativo; }
            set
            {
                if (value != null && value.Length < 2)
                    ativo = value;
            }
        }



    }
}