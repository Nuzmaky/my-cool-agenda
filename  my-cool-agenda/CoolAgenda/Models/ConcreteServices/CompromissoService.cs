﻿using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
using System.Data.Common;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace CoolAgenda.Models
{
    public class CompromissoService : ICompromissoService
    {
        ICompromissoDAO compromissoDAO;
        ICompromissoUsuarioDAO compromissoUsuarioDAO;
        ICompromissoContatoDAO compromissoContatoDAO;
        IUsuarioDAO usuarioDAO;

        public CompromissoService()
        {
            compromissoDAO = new CompromissoDAO();
            compromissoUsuarioDAO = new CompromissoUsuarioDAO();
            compromissoContatoDAO = new CompromissoContatoDAO();
            usuarioDAO = new UsuarioDAO();
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

                    Usuario reg = usuarioDAO.BuscarPorId(cUsuario.IdUsuario, transaction);

                    // Enviar e-mail
                    EnviaEmailCompromisso(cUsuario.IdCompromisso, cUsuario.IdUsuario, reg.Email, reg.Nome);
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

        // Envia e-mail de Tarefa
        public static void EnviaEmailCompromisso(int idCompromisso, int idUsuario, string email, string nome)
        {
            Contato contato = new Contato();
            //objeto responsável pela mensagem de email
            MailMessage objEmail = new MailMessage();

            //rementente do email
            objEmail.From = new MailAddress("mateus.quintino@gmail.com", "Cool Agenda");

            //destinatário(s) do email(s). Obs. pode ser mais de um, pra isso basta repetir a linha
            //abaixo com outro endereço
            objEmail.To.Add(email);

            //prioridade do email
            objEmail.Priority = MailPriority.Normal;

            //utilize true pra ativar html no conteúdo do email, ou false, para somente texto      
            objEmail.IsBodyHtml = false;

            //Assunto do email        
            objEmail.Subject = "Cool Agenda - Novo Compromisso";

            //corpo do email a ser enviado        
            objEmail.Body = "Olá " + nome + "! Você acaba de ser incluso em um Compromisso na Cool Agenda! \n " +
                            "\n \nPara aceitar o seu compromisso, clique no link logo abaixo. \n \n" +

                            "\n\nCaso o link não esteja funcionando, copie e cole na barra de endereços do seu navegador. " +
                            "http://localhost:52333/Compromisso/AceitarCompromisso?id=" + idCompromisso + "&idUser=" + idUsuario + "&u=S" +
                            "http://localhost:52333/Compromisso/NegarCompromisso?id=" + idCompromisso + "&idUser=" + idUsuario +"&u=S";
                                

            //codificação do ASSUNTO do email para que os caracteres acentuados serem reconhecidos.
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");

            //codificação do CORPO do email para que os caracteres acentuados serem reconhecidos.
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            //cria o objeto responsável pelo envio do email
            SmtpClient objSmtp = new SmtpClient();

            //endereço do servidor SMTP(para mais detalhes leia abaixo do código)
            objSmtp.Host = "smtp.gmail.com";

            //para envio de email autenticado, coloque login e senha de seu servidor de email
            //para detalhes leia abaixo do código
            objSmtp.Credentials = new NetworkCredential("mateus.quintino@gmail.com", "quintinuvy");
            objSmtp.EnableSsl = true;
            objSmtp.Port = 587;

            //envia o email
            objSmtp.Send(objEmail);
        }

        // Envia e-mail de Tarefa
        public static void EnviaEmailContato(int idCompromisso, int idContato, string email, string nome)
        {
            Contato contato = new Contato();
            //objeto responsável pela mensagem de email
            MailMessage objEmail = new MailMessage();

            //rementente do email
            objEmail.From = new MailAddress("mateus.quintino@gmail.com", "Cool Agenda");

            //destinatário(s) do email(s). Obs. pode ser mais de um, pra isso basta repetir a linha
            //abaixo com outro endereço
            objEmail.To.Add(email);

            //prioridade do email
            objEmail.Priority = MailPriority.Normal;

            //utilize true pra ativar html no conteúdo do email, ou false, para somente texto      
            objEmail.IsBodyHtml = false;

            //Assunto do email        
            objEmail.Subject = "Cool Agenda - Novo Compromisso";

            //corpo do email a ser enviado        
            objEmail.Body = "Olá " + nome + "! Você acaba de ser incluso em um Compromisso na Cool Agenda! \n " +
                            "\n \nPara aceitar o seu compromisso, clique no link logo abaixo. \n \n" +

                            "\n\nCaso o link não esteja funcionando, copie e cole na barra de endereços do seu navegador. " +
                            "http://localhost:52333/Compromisso/AceitarCompromisso?id=" + idCompromisso + "&idUser=" + idContato + "&u=N" +
                            "http://localhost:52333/Compromisso/NegarCompromisso?id=" + idCompromisso + "&idUser=" + idContato + "&u=N";

            //codificação do ASSUNTO do email para que os caracteres acentuados serem reconhecidos.
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");

            //codificação do CORPO do email para que os caracteres acentuados serem reconhecidos.
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            //cria o objeto responsável pelo envio do email
            SmtpClient objSmtp = new SmtpClient();

            //endereço do servidor SMTP(para mais detalhes leia abaixo do código)
            objSmtp.Host = "smtp.gmail.com";

            //para envio de email autenticado, coloque login e senha de seu servidor de email
            //para detalhes leia abaixo do código
            objSmtp.Credentials = new NetworkCredential("mateus.quintino@gmail.com", "quintinuvy");
            objSmtp.EnableSsl = true;
            objSmtp.Port = 587;

            //envia o email
            objSmtp.Send(objEmail);
        }

        public void Excluir(int id)
        {
            compromissoDAO.Excluir(id);
        }
    }
}