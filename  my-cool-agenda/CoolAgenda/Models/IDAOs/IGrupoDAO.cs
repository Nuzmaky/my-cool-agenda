using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public interface IGrupoDAO
    {
        void Insert(Grupo grupo);

        void Update(Grupo grupo, DbTransaction transacao);

        void DeleteById(int id, DbTransaction transacao);

        List<Grupo> Select();
    }
}