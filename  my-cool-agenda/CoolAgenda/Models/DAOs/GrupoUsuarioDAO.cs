using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class GrupoUsuarioDAO : IGrupoUsuarioDAO
    {
        private string SQL;

        //Insert
        public void Adicionar(GrupoUsuario grupoUser, DbTransaction transaction = null)
        {
            SQL = "INSERT INTO UsuarioGrupo (IdUsuario, IdGrupo, Administrador, Ativo) VALUES (?, ?, ?, ?)";

            OleDbCommand comando = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);
            if (transaction != null)
                comando.Transaction = transaction as OleDbTransaction;

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.Integer);
            pIdUsuario.Value = grupoUser.IdUsuario;
            comando.Parameters.Add(pIdUsuario);

            OleDbParameter pIdGrupo = new OleDbParameter("IdGrupo", OleDbType.Integer);
            pIdGrupo.Value = grupoUser.IdGrupo;
            comando.Parameters.Add(pIdGrupo);

            OleDbParameter pAdministrador = new OleDbParameter("Administrador", OleDbType.VarChar);
            pAdministrador.Value = grupoUser.Administrador;
            comando.Parameters.Add(pAdministrador);

            OleDbParameter pAtivo = new OleDbParameter("Ativo", OleDbType.VarChar);
            pAtivo.Value = grupoUser.Ativo;
            comando.Parameters.Add(pAtivo);

            comando.ExecuteNonQuery();
            comando.Dispose();
        }

    }
}