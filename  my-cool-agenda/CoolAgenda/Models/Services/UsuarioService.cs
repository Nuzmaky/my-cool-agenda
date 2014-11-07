using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}