using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoolAgenda.Models
{
    public interface ITelefoneService
    {
        List<Telefone> Select();
        List<Telefone> ListarPorIdContato(int id);
        List<Telefone> ListarPorIdUsuario(int id);
    }
}