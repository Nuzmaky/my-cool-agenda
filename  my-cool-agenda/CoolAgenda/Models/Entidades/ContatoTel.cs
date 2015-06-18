using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;

namespace CoolAgenda.Models.Entidades
{
    public class ContatoTel
    {
        private ContatoService contatoservice;
        private TelefoneService telefoneservice;

        public void InsereContatoTel(Contato contato, List<Telefone> telefones)
        {
            contatoservice = new ContatoService();
            contatoservice.InsertContato(contato);
            contatoservice.Insert(contato, telefones);
        }

        public void UpdateContatoTel(Contato contato, List<Telefone> telefones)
        {
            contatoservice.UpdateContato(contato);
            contatoservice.Update(contato, telefones);
        }
    }
}
