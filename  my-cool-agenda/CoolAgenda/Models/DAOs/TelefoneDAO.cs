using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class TelefoneDAO : ITelefoneDAO
    {

        private string SQL;

        // Insert
        public void Insert(Telefone entidade)
        {
            SQL = "INSERT INTO Telefone (IdTelefone, IdContato, NumeroTelefone) VALUES (seqTelefone.nextval, ?, ?)";

            // Configura o comando            
            OleDbCommand comando = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);         

            OleDbParameter pIdTelefone = new OleDbParameter("IdTelefone", OleDbType.Integer);
            pIdTelefone.Value = entidade.IdTelefone;
            comando.Parameters.Add(pIdTelefone);

            OleDbParameter pIdContato = new OleDbParameter("IdContato", OleDbType.Integer);
            pIdContato.Value = entidade.IdContato;
            comando.Parameters.Add(pIdContato);

            OleDbParameter pNumeroTelefone = new OleDbParameter("NumeroTelefone", OleDbType.VarChar);
            pNumeroTelefone.Value = entidade.NumeroTelefone;
            comando.Parameters.Add(pNumeroTelefone);

            // Insert
            comando.ExecuteNonQuery();
            comando.Dispose();
        }


        //Update
        public void Update(Telefone entidade)
        {
            SQL = "UPDATE Telefone SET IdContato = ?, NumeroTelefone = ? WHERE IdTelefone = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = SQL;

            OleDbParameter pIdContato = new OleDbParameter("IdContato", OleDbType.Integer);
            pIdContato.Value = entidade.IdContato;
            comando.Parameters.Add(pIdContato);

            OleDbParameter pNumeroTelefone = new OleDbParameter("NumeroTelefone", OleDbType.VarChar);
            pNumeroTelefone.Value = entidade.NumeroTelefone;
            comando.Parameters.Add(pNumeroTelefone);

            OleDbParameter pIdTelefone = new OleDbParameter("IdTelefone", OleDbType.Integer);
            pIdTelefone.Value = entidade.IdTelefone;
            comando.Parameters.Add(pIdTelefone);

            // Update
            comando.ExecuteNonQuery();
            comando.Dispose();
        }


        //Conversão
        public Telefone ConverterParaTipoClasse(OleDbDataReader dr)
        {
            Telefone telefone = new Telefone();
            telefone.IdContato = int.Parse(dr["IdContato"].ToString());
            telefone.IdTelefone = int.Parse(dr["IdTelefone"].ToString());
            telefone.NumeroTelefone = dr["NumeroTelefone"].ToString();

            return telefone;
        }

        //Select
        public List<Telefone> Select()
        {
            List<Telefone> ListaTelefone= new List<Telefone>();
            string SQL = "Select * From Telefone";
            OleDbCommand Select = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            OleDbDataReader dr = Select.ExecuteReader();

            while (dr.Read())
            {
                ListaTelefone.Add(ConverterParaTipoClasse(dr));
            }

            return ListaTelefone;
        }

    }
}