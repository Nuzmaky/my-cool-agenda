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
        void Insert(Contato entidade);
                            
        void Update(Contato entidade);

        void DeleteById(int id);

        List<Contato> Select(); 
        List<Contato> BuscarPorIdUsuario(int id);
        
        Contato BuscarPorId(int id);


        

    }
}