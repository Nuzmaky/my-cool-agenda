using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Data.Common;


namespace CoolAgenda.Models
{
    public class ContatoService : IContatoService
    {
        private IContatoDAO contatoDAO;
        private ITelefoneDAO telefoneDAO;
        private IUsuarioDAO usuarioDAO;
        private IGrupoDAO grupoDAO;
        private IGrupoUsuarioDAO grupoUsuarioDAO;
        private IUsuarioService usuarioService;

        public ContatoService()
        {
            contatoDAO = new ContatoDAO();
            grupoDAO = new GrupoDAO();
            grupoUsuarioDAO = new GrupoUsuarioDAO();
            telefoneDAO = new TelefoneDAO();
            usuarioDAO = new UsuarioDAO();
            usuarioService = new UsuarioService();
        }

        public List<Contato> Select()
        {
            return contatoDAO.Select();
        }


        public void InsertContato(Contato contato)
        {
            contatoDAO.Insert(contato);
        }

        public void UpdateContato(Contato contato)
        {
            contatoDAO.Update(contato);
        }      

        // Responsável por adicionar a lista de Telefones do Contato
        public void Insert(Contato contato, List<Telefone> telefones)
        {
            DbTransaction transacao = Conexao.getConexao().BeginTransaction();
            try
            {
                foreach (var telefone in telefones)
                {
                    InsertTelefone(contato, telefone, transacao);
                }

                // Se chegar até aqui deu tudo certo
                // então realiza o Commit 
                transacao.Commit();
            }
            catch (Exception)
            {
                transacao.Rollback();
                throw;
            }
        }

        public void Update(Contato contato, List<Telefone> telefones)
        {
            DbTransaction transacao = Conexao.getConexao().BeginTransaction();            

            try
            {
                foreach (var telefone in telefones)
                {                                                            
                    UpdateTelefone(contato, telefone, transacao);
                }

                // Se chegar até aqui deu tudo certo
                // então realiza o Commit 
                transacao.Commit();

            }
            catch (Exception)
            {
                transacao.Rollback();
                throw;
            }
        }

        // Responsável por adicionar a Lista de Grupos ao Contato
        public void InsertContatoGrupo(List<GrupoUsuario> listaGrupoUsuario)
        {
            DbTransaction transacao = Conexao.getConexao().BeginTransaction();
            try
            {
                foreach (var grupoUsuario in listaGrupoUsuario)
                {
                    InsertGrupoUsuario(grupoUsuario,transacao);
                }

                // Se chegar até aqui deu tudo certo
                // então realiza o Commit 
                transacao.Commit();
            }
            catch (Exception)
            {
                transacao.Rollback();
                throw;
            }
        }

        // Chama a função que adicionar a lista de Grupos ao contato
        public void InsertGrupoUsuario(GrupoUsuario grupoUsuario, DbTransaction transacao)
        {
            grupoUsuarioDAO.Adicionar(grupoUsuario, transacao);
        }


        // Chama a função que adicionar a lista de Telefones ao contato
        public void InsertTelefone(Contato contato, Telefone entidade, DbTransaction transacao)
        {
            telefoneDAO.Insert(contato, entidade, transacao);
        }

        public void UpdateTelefone(Contato contato, Telefone entidade, DbTransaction transacao)
        {            
            telefoneDAO.Update(contato, entidade, transacao);
        }

        public List<Telefone> BuscarTelefonePorId(int id)
        {
            List<Telefone> telefonesEncontados = telefoneDAO.Select().FindAll(t => t.IdContato == id);
            return telefonesEncontados;
        }

        public void InativarContato(int id)
        {
            contatoDAO.InativarContato(id);
        }

        public Contato BuscarPorId(int id)
        {
            return contatoDAO.BuscarPorId(id);
        }

        public int ProximoIdUser(DbTransaction transaction)
        {
            return usuarioDAO.ProximoIdUser(transaction);
        }

        public int ProximoIdGrupo(DbTransaction transaction)
        {
            return grupoDAO.ProximoIdGrupo(transaction);
        }

        public List<Contato> BuscarPorIdUsuario(int id)
        {
            return contatoDAO.BuscarPorIdUsuario(id);
        }

        public void AddGrupoUser(GrupoUsuario grupoUser, DbTransaction transaction)
        {
            grupoUsuarioDAO.Adicionar(grupoUser, transaction);
        }


        // Envia e-mail de confirmação de Cadastro
        public static void EnviaEmailCadastro(string email, string nome, string senha)
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
            objEmail.Subject = "Cool Agenda - Convite";

            //corpo do email a ser enviado        
            objEmail.Body = "Olá " + nome + "! Você acaba de ser convidado para participar da Cool Agenda! \n " +
                            "\n \nPara confirmar a sua participação, clique no link logo abaixo, e confirme seu cadastro. \n \n" +
                            "Seus dados de login são: \n" +
                            "\nEmail: " + email +
                            "\nSenha: " + senha +
                            "\n\nCaso o link não esteja funcionando, copie e cole na barra de endereços do seu navegador. " +
                            "http://localhost:52333/Usuario/DadosAtivacaoCadastro";

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


        public List<Validacao> ValidaAtualizar(Contato entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            bool existeEmail = contatoDAO.Select().Any(e => e.Email.Equals(entidade.Email, StringComparison.InvariantCultureIgnoreCase)
                && e.IdContato != entidade.IdContato);
            if (existeEmail)
                erros.Add(new Validacao("Já existe um registro com o E-mail informado."));

            return erros;
        }

        public List<Validacao> ValidarEntidade(Contato entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            bool edicao = false;
            if (entidade.IdContato != 0)
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

        public List<Validacao> ValidaAdicionar(Contato entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            bool existeEmail = contatoDAO.Select().Any(e => e.Nome.Equals(entidade.Email, StringComparison.InvariantCultureIgnoreCase));
            if (existeEmail)
                erros.Add(new Validacao("Já existe um registro com o e-mail informado."));

            return erros;
        }

        public List<Validacao> ValidaAdicionarUsuario(string email)
        {
            List<Validacao> erros = new List<Validacao>();

            bool existeEmail = usuarioDAO.Listar().Any(e => e.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
            if (existeEmail)
                erros.Add(new Validacao("Já existe um registro com o e-mail informado."));

            return erros;
        }

        public List<Contato> ListarContatosUsuario(int idUser, string q)
        {
            return contatoDAO.ListarContatosUsuario(idUser, q);
        }
    }

}