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
    }
}