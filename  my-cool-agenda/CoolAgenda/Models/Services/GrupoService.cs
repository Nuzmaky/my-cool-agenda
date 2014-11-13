using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
using System.Data.Common;

namespace CoolAgenda.Models
{
    public class GrupoService : IGrupoService
    {
        private IGrupoDAO grupoDAO;
        private IUsuarioDAO usuarioDAO;

        public GrupoService()
        {
            grupoDAO = new GrupoDAO();
            usuarioDAO = new UsuarioDAO();
        }

        public List<Grupo> Listar()
        {
            return grupoDAO.Listar();
        }
        
        public void Adicionar(Grupo grupo, Usuario user)
        {
            DbTransaction transaction = Conexao.getConexao().BeginTransaction();
            try
            {
                int idGrupo = ProximoIdGrupo(transaction);
                grupo.IdGrupo = idGrupo;
                AddGrupo(grupo, transaction);

                int idUsuario = ProximoIdUser(transaction);
                user.IdUsuario = idUsuario;
                AddUsuario(user, transaction);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public int ProximoIdGrupo(DbTransaction transaction)
        {
            return grupoDAO.ProximoIdGrupo(transaction);
        }

        public int ProximoIdUser(DbTransaction transaction)
        {
            return usuarioDAO.ProximoIdUser(transaction);
        }

        public void AddGrupo(Grupo grupo, DbTransaction transaction)
        {
            grupoDAO.Adicionar(grupo, transaction);
        }

        public void AddUsuario(Usuario user, DbTransaction transaction)
        {
            usuarioDAO.Adicionar(user, transaction);
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
                p => p.CNPJ.Equals(entidade.CNPJ) && p.IdGrupo != entidade.IdGrupo);

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