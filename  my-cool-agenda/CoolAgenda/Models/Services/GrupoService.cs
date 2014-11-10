using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.Models
{
    public class GrupoService : IGrupoService
    {
        private IGrupoDAO grupoDAO;

        public GrupoService()
        {
            grupoDAO = new GrupoDAO();
        }

        public List<Grupo> Listar()
        {
            return grupoDAO.Listar();
        }

        public void Adicionar(Grupo entidade)
        {
            grupoDAO.Adicionar(entidade);
        }

        public void Atualizar(Grupo entidade)
        {
            grupoDAO.Atualizar(entidade);
        }

        public Grupo BuscarPorId(int id)
        {
            return grupoDAO.BuscarPorId(id);
        }

        public List<Validacao> ValidaAtualizar(Grupo entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            bool existeNome = grupoDAO.Listar().Any(e => e.Nome.Equals(entidade.Nome, StringComparison.InvariantCultureIgnoreCase)
                && e.IdGrupo != entidade.IdGrupo);
            if (existeNome)
                erros.Add(new Validacao("Já existe um registro com o nome informado."));

            bool existeCNPJ = grupoDAO.Listar().Any(
                p => p.CNPJ.Equals(entidade.CNPJ));

            if (existeCNPJ)
                erros.Add(new Validacao("Já existe uma empresa com o CNPJ informado."));

            return erros;
        }

        public List<Validacao> ValidarEntidade(Grupo entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            bool edicao = false;
            if (entidade.IdGrupo != 0)
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

        public List<Validacao> ValidaAdicionar(Grupo entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            bool existeNome = grupoDAO.Listar().Any(e => e.Nome.Equals(entidade.Nome, StringComparison.InvariantCultureIgnoreCase));
            if (existeNome)
                erros.Add(new Validacao("Já existe um registro com o nome informado."));

            bool existeCNPJ = grupoDAO.Listar().Any(
                p => p.CNPJ.Equals(entidade.CNPJ));

            if (existeCNPJ)
                erros.Add(new Validacao("Já existe uma empresa com o CNPJ informado."));

            return erros;
        }



    }
}