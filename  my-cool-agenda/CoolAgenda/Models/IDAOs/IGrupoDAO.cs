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
        void Adicionar(Grupo entidade);

        void Atualizar(Grupo entidade);

        List<Grupo> Listar();

        Grupo BuscarPorId(int id);
    }
}