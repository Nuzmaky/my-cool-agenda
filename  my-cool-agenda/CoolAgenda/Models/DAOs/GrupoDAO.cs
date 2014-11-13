using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class GrupoDAO : IGrupoDAO
    {
        private string SQL;

        //Insert
        public void Adicionar(Grupo grupo, DbTransaction transaction = null)
        {
            SQL = "INSERT INTO Grupo (IdGrupo, Nome, CNPJ, Ativo) VALUES (?, ?, ?, ?)";

            OleDbCommand comando = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);
            if (transaction != null)
                comando.Transaction = transaction as OleDbTransaction;

            OleDbParameter pIdGrupo = new OleDbParameter("IdGrupo", OleDbType.Integer);
            pIdGrupo.Value = grupo.IdGrupo;
            comando.Parameters.Add(pIdGrupo);

            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = grupo.Nome;
            comando.Parameters.Add(pNome);

            OleDbParameter pCNPJ = new OleDbParameter("CNPJ", OleDbType.VarChar);
            pCNPJ.Value = grupo.CNPJ;
            comando.Parameters.Add(pCNPJ);

            OleDbParameter Ativo = new OleDbParameter("Ativo", OleDbType.VarChar);
            Ativo.Value = grupo.FlagAtivo;
            comando.Parameters.Add(Ativo);

            comando.ExecuteNonQuery();
            comando.Dispose();
        }

        //Select
        public List<Grupo> Listar()
        {
            String sqlConsulta = "Select * from Grupo";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlConsulta;

            // Select
            OleDbDataReader dr = comando.ExecuteReader();

            List<Grupo> registros = new List<Grupo>();
            while (dr.Read())
            {
                Grupo registro = ConverterDataReaderParaObj(dr);
                registros.Add(registro);
            }
            dr.Close();
            comando.Dispose();

            return registros;
        }

        private Grupo ConverterDataReaderParaObj(OleDbDataReader dr)
        {
            Grupo registro = new Grupo();
            registro.IdGrupo = Int32.Parse(dr["IdGrupo"].ToString());
            registro.Nome = dr["Nome"].ToString();
            registro.CNPJ = dr["CNPJ"].ToString();
            registro.FlagAtivo = dr["Ativo"].ToString();
            return registro;
        }

        //Update
        public void Atualizar(Grupo grupo)
        {
            SQL = "UPDATE Grupo SET Nome = ?, CNPJ = ?, Ativo = ? where IdGrupo = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = SQL;

            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = grupo.Nome;
            comando.Parameters.Add(pNome);

            OleDbParameter pCNPJ = new OleDbParameter("CNPJ", OleDbType.VarChar);
            pCNPJ.Value = grupo.CNPJ;
            comando.Parameters.Add(pCNPJ);

            OleDbParameter pAtivo = new OleDbParameter("Ativo", OleDbType.VarChar);
            pAtivo.Value = grupo.FlagAtivo;
            comando.Parameters.Add(pAtivo);

            OleDbParameter pIdGrupo = new OleDbParameter("IdGrupo", OleDbType.Integer);
            pIdGrupo.Value = grupo.IdGrupo;
            comando.Parameters.Add(pIdGrupo);

            // Update
            comando.ExecuteNonQuery();
            comando.Dispose();
        }


        //Conversão
        public Grupo ConverterParaTipoClasse(OleDbDataReader dr)
        {
            Grupo grupo = new Grupo();
            grupo.IdGrupo = int.Parse(dr["IdGrupo"].ToString());
            grupo.Nome = dr["Nome"].ToString();

            return grupo;
        }

        public Grupo BuscarPorId(int id)
        {
            Grupo registro = null;
            string sqlBuscar = "select * from Grupo where IdGrupo = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlBuscar;

            OleDbParameter pId = new OleDbParameter("IdGrupo", OleDbType.Integer);
            pId.Value = id;
            comando.Parameters.Add(pId);

            // Select
            OleDbDataReader dr = comando.ExecuteReader();
            if (dr.Read())
            {
                registro = ConverterDataReaderParaObj(dr);
            }
            dr.Close();
            comando.Dispose();

            return registro;
        }

        public int ProximoIdGrupo(DbTransaction transaction = null)
        {
                SQL = "select seqGrupo.nextval from dual";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);
            if (transaction != null)
                comando.Transaction = transaction as OleDbTransaction;

            // Select
            OleDbDataReader dr = comando.ExecuteReader();

            if (dr.Read())
            {
                int id = Convert.ToInt32(dr["NEXTVAL"]);
                dr.Close();
                comando.Dispose();
                return id;
            }

            dr.Close();
            comando.Dispose();
            throw new Exception("Erro ao recuperar o próximo ID do Grupo.");
        }

    }
}