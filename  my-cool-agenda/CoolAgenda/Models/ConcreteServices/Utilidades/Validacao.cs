using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class Validacao
    {
        public string Nome { get; set; }
        public string Mensagem { get; set; }

        public Validacao()
        {

        }

        public Validacao(string mensagem)
        {
            Nome = string.Empty;
            Mensagem = mensagem;
        }

        public Validacao(string nome, string mensagem)
        {
            Nome = nome;
            Mensagem = mensagem;
        }
    }
}