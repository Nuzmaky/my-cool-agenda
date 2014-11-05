using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.Models
{
    public class CompromissoService : ICompromissoService
    {
        ICompromissoDAO compromissoDAO;

        public CompromissoService()
        {
            compromissoDAO = new CompromissoDAO();
        }

        public void Adicionar(Compromisso entidade)
        {
            compromissoDAO.Adicionar(entidade);
        }

        public void Atualizar(Compromisso entidade)
        {
            compromissoDAO.Atualizar(entidade);
        }


        public List<Validacao> ValidarEntidade(Compromisso entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            bool edicao = false;
            if (entidade.IdCompromisso != 0)
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

        public List<Validacao> ValidaAdicionar(Compromisso entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            bool dataInvalida = entidade.DataInicial <= DateTime.Now || entidade.DataInicial >= entidade.DataFinal;
            if (dataInvalida)
                erros.Add(new Validacao("Não é possível cadastrar compromisso na data informada"));

            return erros;
        }

        public List<Validacao> ValidaAtualizar(Compromisso entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            bool dataInvalida = entidade.DataInicial <= DateTime.Now || entidade.DataInicial >= entidade.DataFinal;
            if (dataInvalida)
                erros.Add(new Validacao("Não é possível cadastrar compromisso na data informada"));

            return erros;
        }
    }
}