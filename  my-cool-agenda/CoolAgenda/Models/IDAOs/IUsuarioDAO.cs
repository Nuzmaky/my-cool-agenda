using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public interface IUsuarioDAO
    {
        void Insert(Usuario usuario);

        List<Usuario> Select();
    }
}