using CoolAgenda.Models;
using System;
using System.Data.Common;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.Models
{
    public class TarefaService : ITarefaService
    {
        private ITarefaDAO tarefaDao;

        public TarefaService()
        {
            tarefaDao = new TarefaDAO();
        }

        public List<Tarefa> Listar()
        {
            return tarefaDao.Listar();
        }

        public void Adicionar(Tarefa entidade)
        {
            tarefaDao.Adcionar(entidade);
        }

        public void Atualizar(Tarefa entidade)
        {
            tarefaDao.Update(entidade);
        }

         public List<Validacao> ValidaAtualizar(Tarefa entidade)
        {
            List<Validacao> erros = new List<Validacao>();

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

            return erros;
        }

        public Tarefa BuscarPorId(int id)
        {
            return tarefaDao.BuscarPorId(id);
        }

        public List<Tarefa> ListarId(int idUser)
        {
            return tarefaDao.ListarId(idUser);
        }

        //public List<Tarefa> ListarPorGrupo(int idUser, int id)
        //{
        //    return tarefaDao.ListarPorGrupo(idUser, id);
        //}

    }
}