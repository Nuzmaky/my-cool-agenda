using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.Models
{
    public class TarefaService : ITarefaService
    {
        private TarefaDAO tarefaDao;

        public TarefaService()
        {
            tarefaDao = new TarefaDAO();
        }

        public List<Tarefa> Listar()
        {
            return tarefaDao.Listar();
        }

        public void Adicionar(Grupo entidade)
        {
            tarefaDao.Adicionar(entidade);
        }

        public void Atualizar(Grupo entidade)
        {
            tarefaDao.Atualizar(entidade);
        }

        public Grupo BuscarPorId(int id)
        {
            return tarefaDao.BuscarPorId(id);
        }

        public List<Validacao> ValidaAtualizar(Tarefa entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            bool existeNome = tarefaDao.Listar().Any(e => e.Nome.Equals(entidade.NomeTarefa, StringComparison.InvariantCultureIgnoreCase)
                && e.IdGrupo != entidade.IdTarefa);
            if (existeNome)
                erros.Add(new Validacao("Já existe um registro com o nome informado."));

            return erros;
        }

        public List<Validacao> ValidarEntidade(Tarefa entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            bool edicao = false;
            if (entidade.IdTarefa != 0)
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

        public List<Validacao> ValidaAdicionar(Tarefa entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            bool existeNome = tarefaDao.Listar().Any(e => e.Nome.Equals(entidade.NomeTarefa, StringComparison.InvariantCultureIgnoreCase));
            if (existeNome)
                erros.Add(new Validacao("Já existe um registro com o nome informado."));
        }

    }
}