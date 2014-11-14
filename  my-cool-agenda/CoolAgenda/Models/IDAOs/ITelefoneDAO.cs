using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public interface ITelefoneDAO
    {
        void Insert(Telefone entidade);
        List<Telefone> Select();
    }
}