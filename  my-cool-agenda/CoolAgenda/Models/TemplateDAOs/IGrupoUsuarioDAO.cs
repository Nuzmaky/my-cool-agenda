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

        List<GrupoUsuario> ListarUsuarioPorGrupo(int idGrupo, string q, int idUser);

        List<GrupoUsuario> Listar(int id);

        GrupoUsuario validaAdm(int id, int idUser);

        void AtivarPorId(int id);

        void DesativarPorId(int id);

        void DarPermissaoPorId(int id);

        void RetirarPermissaoPorId(int id);

    }
}