using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CoolAgenda.Models
{
    public class CompromissoUsuarioService : ICompromissoUsuarioService
    {
        ICompromissoUsuarioDAO compromissoUsuarioDAO;

        public CompromissoUsuarioService()
        {
            compromissoUsuarioDAO = new CompromissoUsuarioDAO();
        }

        public List<CompromissoUsuario> Listar(int idUser)
        {
            return compromissoUsuarioDAO.Listar(idUser);
        }

        public List<CompromissoUsuario> ListarPorGrupo(int idUser, int id)
        {
            return compromissoUsuarioDAO.ListarPorGrupo(idUser, id);
        }

        public CompromissoUsuario BuscarPorId(int id, int idUser)
        {
            return compromissoUsuarioDAO.BuscarPorId(id, idUser);
        }

        public List<CompromissoUsuario> ListarUsuariosDoCompromisso(int id, int idUser)
        {
            return compromissoUsuarioDAO.ListarUsuariosDoCompromisso(id, idUser);
        }

        public void Aceitar(int id, int idUser)
        {
            compromissoUsuarioDAO.Aceitar(id, idUser);
        }

        public void Rejeitar(int id, int idUser)
        {
            compromissoUsuarioDAO.Rejeitar(id, idUser);
        }

        public List<Validacao> ValidaAtualizar(CompromissoUsuario entidade)
        {
            List<Validacao> erros = new List<Validacao>();

            int usuarioCriador = compromissoUsuarioDAO.ListarUsuariosDoCompromisso(entidade.IdCompromisso, entidade.IdUsuario).Count();
            if (usuarioCriador == 0)
                erros.Add(new Validacao("Apenas o usuário que criou pode alterar o compromisso"));

            return erros;
        }


    }
}