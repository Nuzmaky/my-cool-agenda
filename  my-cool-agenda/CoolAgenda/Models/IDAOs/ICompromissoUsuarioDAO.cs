using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public interface ICompromissoUsuarioDAO
    {
        void Adicionar(CompromissoUsuario entidade, DbTransaction transaction);

        List<CompromissoUsuario> Listar(int idUser);

        List<CompromissoUsuario> ListarPorGrupo(int idUser, int id);

    }
}