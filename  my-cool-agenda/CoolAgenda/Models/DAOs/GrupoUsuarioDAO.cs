﻿using CoolAgenda.Models;
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

        private IGrupoDAO grupoDAO;
        private IUsuarioDAO usuarioDAO;

        public GrupoUsuarioDAO()
        {
            grupoDAO = new GrupoDAO();
            usuarioDAO = new UsuarioDAO();
        }

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

        public List<GrupoUsuario> ListarGruposPessoa(int idUser)
        {
            String sqlConsulta = "select * from usergrupo where usergrupoativo='S' and grupoativo = 'S' and IdUsuario = " + idUser;

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlConsulta;

            // Select
            OleDbDataReader dr = comando.ExecuteReader();

            List<GrupoUsuario> registros = new List<GrupoUsuario>();
            while (dr.Read())
            {
                GrupoUsuario registro = ConverterDataReaderParaObj(dr);
                registros.Add(registro);
            }
            dr.Close();
            comando.Dispose();

            foreach (var registro in registros)
            {
                CarregarComposicao(registro);
            }

            return registros;
        }

        private GrupoUsuario ConverterDataReaderParaObj(OleDbDataReader dr)
        {
            GrupoUsuario grupoUser = new GrupoUsuario();
            grupoUser.IdUsuario = Int32.Parse(dr["IdUsuario"].ToString());
            grupoUser.IdGrupo = Int32.Parse(dr["IdGrupo"].ToString());
            grupoUser.Administrador = dr["Administrador"].ToString();
            grupoUser.Ativo = dr["usergrupoativo"].ToString();
            return grupoUser;
        }

        private void CarregarComposicao(GrupoUsuario grupoUser)
        {
            int idGrupo = grupoUser.IdGrupo;
            Grupo grupo = grupoDAO.BuscarPorIdAtivo(idGrupo);
            grupoUser.Grupo = grupo;

            int idUsuario = grupoUser.IdUsuario;
            Usuario user = usuarioDAO.BuscarPorId(idUsuario);
            grupoUser.Usuario = user;

        }

        public List<GrupoUsuario> ListarUsuarioPorGrupo(int idGrupo)
        {
            String sqlConsulta = "select * from usergrupo where usergrupoativo='S' and grupoativo = 'S' and IdGrupo = " + idGrupo;

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlConsulta;

            // Select
            OleDbDataReader dr = comando.ExecuteReader();

            List<GrupoUsuario> registros = new List<GrupoUsuario>();
            while (dr.Read())
            {
                GrupoUsuario registro = ConverterDataReaderParaObj(dr);
                registros.Add(registro);
            }
            dr.Close();
            comando.Dispose();

            foreach (var registro in registros)
            {
                CarregarComposicao(registro);
            }

            return registros;
        }
    }
}