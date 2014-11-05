using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public interface IContatoDAO
    {
        void Insert(Contato contato);

        void Update(Contato contato, DbTransaction transacao);

        void DeleteById(int id, DbTransaction transacao);

        List<Contato> Select();

    }
}