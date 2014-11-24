using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Common;

namespace CoolAgenda.Models
{
    public interface ICompromissoContatoService
    {
        List<CompromissoContato> ListarContatoDoCompromisso(int id);
    }
}