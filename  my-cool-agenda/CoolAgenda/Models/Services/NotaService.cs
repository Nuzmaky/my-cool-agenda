using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models.Services
{
    public class NotaService : INotaService
    {
        private NotaDAO notaDao;

        public NotaService()
        {
            notaDao = new NotaDAO();
        }

        public List<Nota> Listar()
        {
            return notaDao.Listar();
        }

        public void Adicionar(Nota entidade)
        {
            notaDao.Adcionar(entidade);
        }

        public void Update(Nota entidade)
        {
            notaDao.Update(entidade);
        }

         public List<Validacao> ValidaAtualizar(Nota entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            return erros;
        }

        public List<Validacao> ValidarEntidade(Nota entidade)
        {
            List<Validacao> erros = new List<Validacao>();
            bool edicao = false;
            if (entidade.IdNota != 0)
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

        public List<Validacao> ValidaAdicionar(Nota entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            return erros;
        }

        public Nota BuscarPorId(int id)
        {
            return notaDao.BuscarPorId(id);
        }

    }

}
