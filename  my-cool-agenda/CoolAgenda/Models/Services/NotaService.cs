using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class NotaService : INotaService
    {
        private INotaDAO notaDao;
        private ICompromissoUsuarioDAO compromissoUsuarioDAO;

        public NotaService()
        {
            notaDao = new NotaDAO();
            compromissoUsuarioDAO = new CompromissoUsuarioDAO();
        }

        public List<Nota> Listar()
        {
            return notaDao.Listar();
        }

        public List<Nota> Listar(int id)
        {
            return notaDao.Listar(id);
        }

        public void Adicionar(Nota entidade)
        {
            notaDao.Adcionar(entidade);
        }

        public void Update(Nota entidade)
        {
            notaDao.Update(entidade);
        }

         public List<Validacao> ValidaAtualizar(Nota entidade, int id)
        {
            List<Validacao> erros = new List<Validacao>();

            int usuarioCriador = notaDao.VerificarUsuarioCriador(entidade.IdNota, id).Count();
            if (usuarioCriador == 0)
                erros.Add(new Validacao("Apenas o criador do compromisso ou da nota podem fazer a edição."));


            bool verificaLider = compromissoUsuarioDAO.ListarUsuariosDoCompromisso(entidade.IdCompromisso, id).Any(e => e.Criador.Equals("S", StringComparison.InvariantCultureIgnoreCase));
            if (verificaLider) { }

            else
                erros.Clear();

            return erros;
        }

        public List<Validacao> ValidarEntidade(Nota entidade, int id)
        {
            List<Validacao> erros = new List<Validacao>();
            bool edicao = false;
            if (entidade.IdNota != 0)
                edicao = true;
            if (edicao)
            {
                List<Validacao> errosAtualizar = ValidaAtualizar(entidade, id);
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

        public Nota BuscarNotaUsuarioCompromisso(int id, int idUser)
        {
            return notaDao.BuscarNotaUsuarioCompromisso(id, idUser);
        }
    }

}
