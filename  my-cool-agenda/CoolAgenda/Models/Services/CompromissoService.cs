﻿using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
using System.Data.Common;

namespace CoolAgenda.Models
{
    public class CompromissoService : ICompromissoService
    {
        ICompromissoDAO compromissoDAO;
        ICompromissoUsuarioDAO compromissoUsuarioDAO;
        ICompromissoContatoDAO compromissoContatoDAO;

        public CompromissoService()
        {
            compromissoDAO = new CompromissoDAO();
            compromissoUsuarioDAO = new CompromissoUsuarioDAO();
            compromissoContatoDAO = new CompromissoContatoDAO();
        }

        public void Adicionar(Compromisso entidade, List<CompromissoUsuario> cUser, List<CompromissoContato> cContato)
        {

            DbTransaction transaction = Conexao.getConexao().BeginTransaction();
            try
            {
                int idCompromisso = ProximoIdCompromisso(transaction);
                entidade.IdCompromisso = idCompromisso;
                AddCompromisso(entidade, transaction);

                foreach (var cUsuario in cUser)
                {
                    cUsuario.IdCompromisso = idCompromisso;
                    AddCompromissoUsuario(cUsuario, transaction);
                }

                foreach (var cCont in cContato)
                {
                    cCont.IdCompromisso = idCompromisso;
                    AddCompromissoContato(cCont, transaction);
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
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

            if (entidade.DiaInteiro == false)
            {
                bool dataInvalida2 = entidade.DataInicial.AddMinutes(30) > entidade.DataFinal;
                if (dataInvalida2)
                    erros.Add(new Validacao("A Data Final tem que ter ao menos 30 minutos da data Inicial"));
            }

            return erros;
        }

        public List<Validacao> ValidaAtualizar(Compromisso entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            bool dataInvalida = entidade.DataInicial > entidade.DataFinal;
            if (dataInvalida)
                erros.Add(new Validacao("Data Inicial maior que a Data Final"));

            if (entidade.DiaInteiro == false)
            {
                bool dataInvalida2 = entidade.DataInicial.AddMinutes(30) > entidade.DataFinal;
                if (dataInvalida2)
                    erros.Add(new Validacao("A Data Final tem que ter ao menos 30 minutos da data Inicial"));
            }

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

        public int ProximoIdCompromisso(DbTransaction transaction)
        {
            return compromissoDAO.ProximoIdCompromisso(transaction);
        }

        public void AddCompromisso(Compromisso entidade, DbTransaction transaction)
        {
            compromissoDAO.Adicionar(entidade, transaction);
        }

        public void AddCompromissoUsuario(CompromissoUsuario entidade, DbTransaction transaction)
        {
            compromissoUsuarioDAO.Adicionar(entidade, transaction);
        }

        public void AddCompromissoContato(CompromissoContato entidade, DbTransaction transaction)
        {
            compromissoContatoDAO.Adicionar(entidade, transaction);
        }
    }
}