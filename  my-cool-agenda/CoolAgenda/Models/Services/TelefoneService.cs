using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.Models
{
    public class TelefoneService : ITelefoneService
    {

        private ITelefoneDAO telefoneDAO;

        public TelefoneService()
        {
            telefoneDAO = new TelefoneDAO();
        }

        public List<Telefone> Select()
        {
            return telefoneDAO.Select();
        }

        public List<Telefone> ListarPorIdContato(int id)
        {
            return telefoneDAO.ListarPorIdContato(id);
        }

        public List<Telefone> ListarPorIdUsuario(int id)
        {
            return telefoneDAO.ListarPorIdUsuario(id);
        }
    }
}