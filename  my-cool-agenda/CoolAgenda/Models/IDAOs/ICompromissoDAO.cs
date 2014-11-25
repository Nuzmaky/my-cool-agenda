using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public interface ICompromissoDAO
    {
        void Adicionar(Compromisso entidade, DbTransaction transaction);

        void Atualizar(Compromisso entidade);

        Compromisso BuscarPorId(int idCompromisso);

        int ProximoIdCompromisso(DbTransaction transaction);

        void Excluir(int id);
        
    }
}