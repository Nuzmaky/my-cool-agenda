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
        public void Adicionar(Grupo grupo)
        {
            SQL = "INSERT INTO Grupo (IdGrupo, Nome, CNPJ, FlagAtivo) VALUES (SeqGrupo.NEXTVAL, ?, ?, ?)";

            OleDbCommand comando = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = grupo.Nome;
            comando.Parameters.Add(pNome);

            OleDbParameter pCNPJ = new OleDbParameter("CNPJ", OleDbType.VarChar);
            pCNPJ.Value = grupo.CNPJ;
            comando.Parameters.Add(pCNPJ);

            OleDbParameter pFlagAtivo = new OleDbParameter("pFlagAtivo", OleDbType.VarChar);
            pFlagAtivo.Value = grupo.FlagAtivo;
            comando.Parameters.Add(pFlagAtivo);

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
            registro.FlagAtivo = dr["FlagAtivo"].ToString();
            return registro;
        }

        //Update
        public void Atualizar(Grupo grupo)
        {
            SQL = "UPDATE Grupo SET Nome = ?, CNPJ = ?, FlagAtivo = ? where IdGrupo = ?";

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

            OleDbParameter pFlagAtivo = new OleDbParameter("FlagAtivo", OleDbType.VarChar);
            pFlagAtivo.Value = grupo.FlagAtivo;
            comando.Parameters.Add(pFlagAtivo);

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
    }
}