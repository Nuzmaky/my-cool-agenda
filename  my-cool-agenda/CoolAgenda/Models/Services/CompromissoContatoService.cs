using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CoolAgenda.Models
{
    public class CompromissoContatoService : ICompromissoContatoService
    {
        ICompromissoContatoDAO compromissoContatoDAO;

        public CompromissoContatoService()
        {
            compromissoContatoDAO = new CompromissoContatoDAO();
        }

        public List<CompromissoContato> ListarContatoDoCompromisso(int id)
        {
            return compromissoContatoDAO.ListarContatoDoCompromisso(id);
        }

        public void Aceitar(int id, int idContato)
        {
            compromissoContatoDAO.Aceitar(id, idContato);
        }

        public void Rejeitar(int id, int idContato)
        {
            compromissoContatoDAO.Rejeitar(id, idContato);
        }
    }
}