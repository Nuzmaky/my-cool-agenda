using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Data.Common;
using System.Data.OleDb;

namespace CoolAgenda.Models
{
    public class UsuarioService : IUsuarioService
    {       
        private IUsuarioDAO usuarioDAO;

        public UsuarioService()
        {
            usuarioDAO = new UsuarioDAO();
        }
    
        //Autentica o Usuário
        public Usuario AutenticaUsuario (string email, string senha)
        {
            return usuarioDAO.Select().Find(
                        m => (m.Email.Equals(email) && m.Senha.Equals(senha)));
        }

        //Valida o Usuario - ERROS
        public List<ValidationResult> ValidaUsuario(string email, string senha)
        {
            List<ValidationResult> erros = new List<ValidationResult>();

            Usuario usuario = usuarioDAO.Select().Find(
                        m => (m.Email.Equals(email) && m.Senha.Equals(senha)));

            if (usuario == null)
            {
                erros.Add(new ValidationResult("Usuário e/ou senha inválidos."));
            }    
            else
            {
            }

            return erros;
        }

        //Lista de Usuarios
        public List<Usuario> ListarUsuario()
        {
            return usuarioDAO.Select();
        }


        // Envia e-mail de confirmação de Cadastro
        public static void EnviaEmailCadastro(string email, string senha, string nome)
        {
            Usuario usuario = new Usuario();
            //objeto responsável pela mensagem de email
            MailMessage objEmail = new MailMessage();

            //rementente do email
            objEmail.From = new MailAddress("mateus.quintino@gmail.com","Cool Agenda");           

            //destinatário(s) do email(s). Obs. pode ser mais de um, pra isso basta repetir a linha
            //abaixo com outro endereço
            objEmail.To.Add(email);

            //prioridade do email
            objEmail.Priority = MailPriority.Normal;

            //utilize true pra ativar html no conteúdo do email, ou false, para somente texto      
            objEmail.IsBodyHtml = false;

            //Assunto do email        
            objEmail.Subject = "Cool Agenda - Confirmação de Cadastro";

            //corpo do email a ser enviado        
            objEmail.Body = "Olá "+nome +"! Você acaba de se cadastrar na Cool Agenda! \n "+
                            "Seus dados de login são: \n"+
                            "\nEmail: "+email+
                            "\nSenha: "+senha+
                            "\n \nPara confirmar a sua participação, clique no link logo abaixo, e confirme seu cadastro. \n \n"+
                            "\nCaso o link não esteja funcionando, copie e cole na barra de endereços do seu navegador. "+
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

        //Ativar Cadastro
        public bool AtivarCadastro(string email)
        {
            Usuario usuario = usuarioDAO.Select().Find(u => u.Email.Equals(email));

            if (usuario != null)
            {
                usuarioDAO.AtivaCadastro(usuario);
                return true;
            }
            return false;
        }



    }
}