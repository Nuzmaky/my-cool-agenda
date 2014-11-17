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
        int ProximoIdGrupo(DbTransaction transacao);

        void Adicionar(Grupo entidade, DbTransaction transaction);

        void Atualizar(Grupo entidade);

        List<Grupo> Listar();

        Grupo BuscarPorId(int id);

        Grupo BuscarPorIdAtivo(int id);

    }
}