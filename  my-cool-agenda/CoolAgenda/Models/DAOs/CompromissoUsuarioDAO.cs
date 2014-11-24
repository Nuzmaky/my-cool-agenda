using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class CompromissoUsuarioDAO : ICompromissoUsuarioDAO
    {
        private ICompromissoDAO compromissoDAO;
        private IUsuarioDAO usuarioDAO;
        private IGrupoDAO grupoDAO;

        public CompromissoUsuarioDAO()
        {
            compromissoDAO = new CompromissoDAO();
            usuarioDAO = new UsuarioDAO();
            grupoDAO = new GrupoDAO();
        }


        public void Adicionar(CompromissoUsuario entidade, DbTransaction transaction = null)
        {
            string sqlAdicionar = "INSERT INTO CompromissoUsuario (IdUsuario, IdGrupo, IdCompromisso, Criador, Aceito) VALUES (?, ?, ?, ?, ?)";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand(sqlAdicionar, Conexao.getConexao() as OleDbConnection);
            if (transaction != null)
                comando.Transaction = transaction as OleDbTransaction;

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.Integer);
            pIdUsuario.Value = entidade.IdUsuario;
            comando.Parameters.Add(pIdUsuario);

            OleDbParameter pIdGrupo = new OleDbParameter("IdGrupo", OleDbType.Integer);
            pIdGrupo.Value = entidade.IdGrupo;
            comando.Parameters.Add(pIdGrupo);

            OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.Integer);
            pIdCompromisso.Value = entidade.IdCompromisso;
            comando.Parameters.Add(pIdCompromisso);

            OleDbParameter pCriador = new OleDbParameter("Criador", OleDbType.VarChar);
            pCriador.Value = entidade.Criador;
            comando.Parameters.Add(pCriador);

            OleDbParameter pAceito = new OleDbParameter("Aceito", OleDbType.VarChar);
            pAceito.Value = entidade.Ativo;
            comando.Parameters.Add(pAceito);

            //Insert
            comando.ExecuteNonQuery();
            comando.Dispose();
        }

        public List<CompromissoUsuario> Listar(int idUser)
        {
            String sqlConsulta = "Select * from CompromissoUser where IdUsuario = ? and Ativo = 'S' ";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlConsulta;

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.Integer);
            pIdUsuario.Value = idUser;
            comando.Parameters.Add(pIdUsuario);

            // Select
            OleDbDataReader dr = comando.ExecuteReader();

            List<CompromissoUsuario> registros = new List<CompromissoUsuario>();
            while (dr.Read())
            {
                CompromissoUsuario compromissoUser = ConverterDataReaderParaObj(dr);
                registros.Add(compromissoUser);
            }
            dr.Close();
            comando.Dispose();

            foreach (var registro in registros)
            {
                CarregarComposicao(registro);
            }

            return registros;
        }

        public List<CompromissoUsuario> ListarPorGrupo(int idUser, int id)
        {
            String sqlConsulta = "Select * from CompromissoUsuario where IdUsuario = ? and IdGrupo = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlConsulta;

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.Integer);
            pIdUsuario.Value = idUser;
            comando.Parameters.Add(pIdUsuario);

            OleDbParameter pIdGrupo = new OleDbParameter("IdGrupo", OleDbType.Integer);
            pIdGrupo.Value = id;
            comando.Parameters.Add(pIdGrupo);

            // Select
            OleDbDataReader dr = comando.ExecuteReader();

            List<CompromissoUsuario> registros = new List<CompromissoUsuario>();
            while (dr.Read())
            {
                CompromissoUsuario compromissoUser = ConverterDataReaderParaObj(dr);
                registros.Add(compromissoUser);
            }
            dr.Close();
            comando.Dispose();

            foreach (var registro in registros)
            {
                CarregarComposicao(registro);
            }

            return registros;
        }

        private CompromissoUsuario ConverterDataReaderParaObj(OleDbDataReader dr)
        {
            CompromissoUsuario compromissoUser = new CompromissoUsuario();
            compromissoUser.IdUsuario = Int32.Parse(dr["IdUsuario"].ToString());
            compromissoUser.IdGrupo = Int32.Parse(dr["IdGrupo"].ToString());
            compromissoUser.IdCompromisso = Int32.Parse(dr["IdCompromisso"].ToString());
            compromissoUser.Criador = dr["Criador"].ToString();
            compromissoUser.Ativo = dr["Aceito"].ToString();

            return compromissoUser;
        }

        private void CarregarComposicao(CompromissoUsuario compromissoUser)
        {
            int idCompromisso = compromissoUser.IdCompromisso;
            Compromisso compromisso = compromissoDAO.BuscarPorId(idCompromisso);
            compromissoUser.Compromisso = compromisso;

            int idUsuario = compromissoUser.IdUsuario;
            Usuario user = usuarioDAO.BuscarPorId(idUsuario);
            compromissoUser.Usuario = user;

            int idGrupo = compromissoUser.IdGrupo;
            Grupo grupo = grupoDAO.BuscarPorId(idGrupo);
            compromissoUser.Grupo = grupo;
        }

        public CompromissoUsuario BuscarPorId(int id, int idUser)
        {
            CompromissoUsuario registro = null;
            string sqlBuscar = "Select * from CompromissoUser where IdUsuario = ? and IdCompromisso = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlBuscar;

            OleDbParameter pId = new OleDbParameter("IdUsuario", OleDbType.Integer);
            pId.Value = idUser;
            comando.Parameters.Add(pId);

            OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.Integer);
            pIdCompromisso.Value = id;
            comando.Parameters.Add(pIdCompromisso);

            // Select
            OleDbDataReader dr = comando.ExecuteReader();
            if (dr.Read())
            {
                registro = ConverterDataReaderParaObj(dr);
            }
            dr.Close();
            comando.Dispose();

            CarregarComposicao(registro);

            return registro;
        }

        public List<CompromissoUsuario> ListarUsuariosDoCompromisso(int id, int idUser)
        {
            
            String sqlConsulta = "select * from CompromissoUser where IdUsuario != ? and IdCompromisso = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlConsulta;

            OleDbParameter pId = new OleDbParameter("IdUsuario", OleDbType.Integer);
            pId.Value = idUser;
            comando.Parameters.Add(pId);

            OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.Integer);
            pIdCompromisso.Value = id;
            comando.Parameters.Add(pIdCompromisso);

            // Select

            OleDbDataReader dr = comando.ExecuteReader();

            List<CompromissoUsuario> registros = new List<CompromissoUsuario>();
            while (dr.Read())
            {
                CompromissoUsuario registro = ConverterDataReaderParaObj(dr);
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

        public List<CompromissoUsuario> VerificarUsuarioCriador(int id, int idUser)
        {

            String sqlConsulta = "select * from CompromissoUser where IdUsuario = ? and IdCompromisso = ? and Criador = 'S'";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlConsulta;

            OleDbParameter pId = new OleDbParameter("IdUsuario", OleDbType.Integer);
            pId.Value = idUser;
            comando.Parameters.Add(pId);

            OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.Integer);
            pIdCompromisso.Value = id;
            comando.Parameters.Add(pIdCompromisso);

            // Select

            OleDbDataReader dr = comando.ExecuteReader();

            List<CompromissoUsuario> registros = new List<CompromissoUsuario>();
            while (dr.Read())
            {
                CompromissoUsuario registro = ConverterDataReaderParaObj(dr);
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

        public void Aceitar(int id, int idUser)
        {
            String sqlAtualizar = "update CompromissoUsuario set Aceito = 'S' where IdUsuario = " + idUser + " and IdCompromisso = " + id;

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlAtualizar;

            // Update
            comando.ExecuteNonQuery();
            comando.Dispose();
        }

        public void Rejeitar(int id, int idUser)
        {
            String sqlAtualizar = "update CompromissoUsuario set Aceito = 'N' where IdUsuario = " + idUser + " and IdCompromisso = " + id;

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlAtualizar;

            // Update
            comando.ExecuteNonQuery();
            comando.Dispose();
        }

    }
}