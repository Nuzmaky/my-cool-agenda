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

            bool dataInvalida = entidade.DataInicial > entidade.DataFinal;
            if (dataInvalida)
                erros.Add(new Validacao("Data Inicial maior que a Data Final"));

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

        public List<SelectListItem> ListarCores()
        {
            List<SelectListItem> itens = new List<SelectListItem>();

            itens.Add(new SelectListItem { Text = "Azul Escuro", Value = "#5484ed" });
            itens.Add(new SelectListItem { Text = "Verde", Value = "#7bd148" });
            itens.Add(new SelectListItem { Text = "Azul", Value = "#a4bdfc" });
            itens.Add(new SelectListItem { Text = "Turquoise", Value = "#46d6db" });
            itens.Add(new SelectListItem { Text = "Verde-Claro", Value = "#7ae7bf" });
            itens.Add(new SelectListItem { Text = "Verde Escuro", Value = "#51b749" });
            itens.Add(new SelectListItem { Text = "Armarelo", Value = "#fbd75b" });
            itens.Add(new SelectListItem { Text = "Laranja", Value = "#ffb878" });
            itens.Add(new SelectListItem { Text = "Vermelho", Value = "#dc2127" });
            itens.Add(new SelectListItem { Text = "Roxo", Value = "#dbadff" });

            return itens;
        }

        public List<Compromisso> Listar()
        {
            return compromissoDAO.Listar();
        }
    }
}