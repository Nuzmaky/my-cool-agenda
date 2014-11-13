using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public interface IGrupoUsuarioDAO
    {
        void Adicionar(GrupoUsuario grupoUser, DbTransaction transaction);
    }
}