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
        public void Insert(Grupo grupo)
        {
            SQL = "INSERT INTO Grupo (IdGrupo, Nome) VALUES (SeqGrupo.NEXTVAL, ?)";

            OleDbCommand comando = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = grupo.Nome;
            comando.Parameters.Add(pNome);

            comando.ExecuteNonQuery();
            comando.Dispose();
        }

        //Select
        public List<Grupo> Select()
        {
            List<Grupo> ListaGrupo = new List<Grupo>();
            string SQL = "Select * From Grupo";
            OleDbCommand Select = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            OleDbDataReader dr = Select.ExecuteReader();

            while (dr.Read())
            {
                ListaGrupo.Add(ConverterParaTipoClasse(dr));
            }

            return ListaGrupo;
        }


        //Update
        public void Update(Grupo grupo, DbTransaction transacao)
        {
            SQL = "UPDATE Grupo SET IdGrupo = ?, Nome = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = SQL;
            if (transacao != null)
                comando.Transaction = transacao as OleDbTransaction;

            OleDbParameter pIdGrupo = new OleDbParameter("IdGrupo", OleDbType.Integer);
            pIdGrupo.Value = grupo.IdGrupo;
            comando.Parameters.Add(pIdGrupo);

            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = grupo.Nome;
            comando.Parameters.Add(pNome);

            // Update
            comando.ExecuteNonQuery();
            comando.Dispose();
        }



        //Delete
        public void DeleteById(int id, DbTransaction transacao)
        {
            SQL = "DELETE Grupo WHERE IdGrupo = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao() as OleDbConnection;
            comando.CommandText = SQL;
            if (transacao != null)
                comando.Transaction = transacao as OleDbTransaction;

            OleDbParameter pId = new OleDbParameter("IdGrupo", OleDbType.VarChar);
            pId.Value = id;
            comando.Parameters.Add(pId);

            // Delete
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


    }
}