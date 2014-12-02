using CoolAgenda.Models;
using System;
using System.Data.Common;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace CoolAgenda.Models
{
    public class TarefaService : ITarefaService
    {
        private ITarefaDAO tarefaDao;

        public TarefaService()
        {
            tarefaDao = new TarefaDAO();
        }

        public List<Tarefa> Listar(int id)
        {
            return tarefaDao.Listar(id);
        }

        public List<Tarefa> ListarReq(int id)
        {
            return tarefaDao.ListarReq(id);
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
            DateTime Inicial = DateTime.Parse(entidade.DataInicial);
            DateTime Final = DateTime.Parse(entidade.DataFinal);
            if (Final < Inicial)
            {
                erros.Add(new Validacao("A Data Final tem que ter Após a Data Inicial"));
            }
            else
            {
                if (Inicial.AddMinutes(30) > Final)
                {
                    erros.Add(new Validacao("A Data Final tem que ser pelo menos 30 minutos após o Inicio da Tarefa"));
                }
            }
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
            DateTime Inicial = DateTime.Parse(entidade.DataInicial);
            DateTime Final = DateTime.Parse(entidade.DataFinal);
            if (Final < Inicial)
            {
                erros.Add(new Validacao("A Data Final tem que ter Após a Data Inicial"));
            }
            else
            {
                if (Inicial.AddMinutes(30) > Final)
                {
                    erros.Add(new Validacao("A Data Final tem que ser pelo menos 30 minutos após o Inicio da Tarefa"));
                }
            }
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

        // Envia e-mail de Tarefa
        public static void EnviaEmailTarefa(string email, string nome)
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
            objEmail.Subject = "Cool Agenda - Nova Tarefa";

            //corpo do email a ser enviado        
            objEmail.Body = "Olá " + nome + "! Você acaba de ser convocado para realizar uma tarefa! \n " +
                            "\n \nPara ver a tarefa, faça o seu login!. \n \n" +

                            "\n\nCaso o link não esteja funcionando, copie e cole na barra de endereços do seu navegador. " +
                            "http://localhost:52333/Index";

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

        public void DesativarPorId(int id)
        {
            tarefaDao.DesativarPorId(id);
        }

        public void ConcluirPorId(int id)
        {
            tarefaDao.ConcluirPorId(id);
        }
    }
}