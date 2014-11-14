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
        void Insert(Contato entidade, DbTransaction transacao);
                            
        void Update(Contato entidade);

        void DeleteById(int id);

        List<Contato> Select(); 
        
        Contato BuscarPorId(int id);

        

    }
}