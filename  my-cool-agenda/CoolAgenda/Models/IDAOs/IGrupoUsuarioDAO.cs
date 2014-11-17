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

        List<GrupoUsuario> ListarGruposPessoa(int idUser);

        List<GrupoUsuario> ListarUsuarioPorGrupo(int idGrupo);
    }
}