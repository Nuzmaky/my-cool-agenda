using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.Models
{
    public class ContatoService : IContatoService
    {
        private IContatoDAO contatoDAO;

        public ContatoService()
        {
            contatoDAO = new ContatoDAO();
        }

        public List<Contato> Select()
        {
            return contatoDAO.Select();
        }

        public void Insert(Contato entidade, List<Telefone> telefone)
        {
            contatoDAO.Insert(entidade);
        }

        public void Update(Contato entidade)
        {
            contatoDAO.Update(entidade);
        }

        public void DeleteById(int id)
        {
            contatoDAO.DeleteById(id);
        }

        public Contato BuscarPorId(int id)
        {
            return contatoDAO.BuscarPorId(id);
        }


        public List<Validacao> ValidaAtualizar(Contato entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            bool existeEmail = contatoDAO.Select().Any(e => e.Email.Equals(entidade.Email, StringComparison.InvariantCultureIgnoreCase)
                && e.IdContato != entidade.IdContato);
            if (existeEmail)
                erros.Add(new Validacao("Já existe um registro com o E-mail informado."));

            return erros;
        }

        public List<Validacao> ValidarEntidade(Contato entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            bool edicao = false;
            if (entidade.IdContato != 0)
                edicao = true;

            if (edicao)
            {
                List<Validacao> errosAtualizar = ValidaAtualizar(entidade);
                if (errosAtualizar != null)
                    erros.AddRange(errosAtualizar);
            }
            else
            {
                List<Validacao> errosAdicionar = ValidaAdicionar(entidade);
                if (errosAdicionar != null)
                    erros.AddRange(errosAdicionar);
            }

            return erros;
        }

        public List<Validacao> ValidaAdicionar(Contato entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            bool existeEmail = contatoDAO.Select().Any(e => e.Nome.Equals(entidade.Email, StringComparison.InvariantCultureIgnoreCase));
            if (existeEmail)
                erros.Add(new Validacao("Já existe um registro com o e-mail informado."));

            return erros;
        }
    }
}