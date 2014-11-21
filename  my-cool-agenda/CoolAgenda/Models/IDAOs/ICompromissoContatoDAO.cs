using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public interface ICompromissoContatoDAO
    {
        void Adicionar(CompromissoContato entidade, DbTransaction transaction);
    }
}