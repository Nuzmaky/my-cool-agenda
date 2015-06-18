using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models.Entidades
{
    public class ContatoTel
    {
        public Contato contato = new Contato();
        public ContatoDAO contatodao = new ContatoDAO();
        public Telefone telefone = new Telefone();
        public TelefoneDAO telefonedao = new TelefoneDAO();

    }
}