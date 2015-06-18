using CoolAgenda.Models;
using CoolAgenda.Models.Entidades;
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
        public void Insert(Contato contato, Telefone telefone, DbTransaction transacao = null)
        {
            SQL = "INSERT INTO Telefone (IdTelefone, IdContato, NumeroTelefone) VALUES (seqTelefone.nextval, ?, ?)";

            // Configura o comando            
            OleDbCommand comando = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            if (transacao != null)
                comando.Transaction = transacao as OleDbTransaction;

            OleDbParameter pIdContato = new OleDbParameter("IdContato", OleDbType.Integer);
            pIdContato.Value = contato.IdContato;
            comando.Parameters.Add(pIdContato);

            OleDbParameter pNumeroTelefone = new OleDbParameter("NumeroTelefone", OleDbType.VarChar);
            pNumeroTelefone.Value = telefone.NumeroTelefone;
            comando.Parameters.Add(pNumeroTelefone);

            // Insert
            comando.ExecuteNonQuery();
            comando.Dispose();
        }


        //Update
        public void Update(Contato contato, Telefone telefone, DbTransaction transacao = null) 
        {
            SQL = "UPDATE Telefone SET NumeroTelefone = ? WHERE IdContato = ? and IdTelefone = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = SQL;

            if (transacao != null)
                comando.Transaction = transacao as OleDbTransaction;

            OleDbParameter pNumeroTelefone = new OleDbParameter("NumeroTelefone", OleDbType.VarChar);
            pNumeroTelefone.Value = telefone.NumeroTelefone;
            comando.Parameters.Add(pNumeroTelefone);
            
            OleDbParameter pIdContato = new OleDbParameter("IdContato", OleDbType.Integer);
            pIdContato.Value = telefone.IdContato;
            comando.Parameters.Add(pIdContato);

            OleDbParameter pIdTelefone = new OleDbParameter("IdTelefone", OleDbType.Integer);
            pIdTelefone.Value = telefone.IdTelefone;
            comando.Parameters.Add(pIdTelefone);

            // Update
            comando.ExecuteNonQuery();
            comando.Dispose();
        }

        //Select
        public List<Telefone> Select()
        {
            List<Telefone> ListaTelefone= new List<Telefone>();
            string SQL = "Select * From Telefone Order By IdTelefone";
            OleDbCommand Select = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            OleDbDataReader dr = Select.ExecuteReader();

            while (dr.Read())
            {
                ListaTelefone.Add(ConverterParaTipoClasse(dr));
            }

            return ListaTelefone;
        }


        public Telefone SelectById(int id)
        {
            Telefone registro = null;
            SQL = "select * from TELEFONE where IdTelefone = ?";
                    
            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = SQL;

            OleDbParameter pId = new OleDbParameter("IdTelefone", OleDbType.Integer);
            pId.Value = id;
            comando.Parameters.Add(pId);

            // Select
            OleDbDataReader dr = comando.ExecuteReader();
            if (dr.Read())
            {
                registro = ConverterParaTipoClasse(dr);
            }
            dr.Close();
            comando.Dispose();

            return registro;
        }

        public List<Telefone> ListarPorIdContato(int idContato)
        {
            SQL = "SELECT * FROM Telefone WHERE IdContato = " + idContato;

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = SQL;

            // Select
            OleDbDataReader dr = comando.ExecuteReader();

            List<Telefone> registros = new List<Telefone>();
            while (dr.Read())
            {
                Telefone registro = ConverterParaTipoClasse(dr);
                registros.Add(registro);
            }
            dr.Close();
            comando.Dispose();

            return registros;
        }


        public List<Telefone> ListarPorIdUsuario(int idUsuario)
        {
            SQL = "SELECT * FROM Telefone T , Contato C "+
                    "WHERE C.IdUsuario = " +idUsuario +
                        " and C.IdContato = T.idcontato Order by C.IdContato";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = SQL;

            // Select
            OleDbDataReader dr = comando.ExecuteReader();

            List<Telefone> registros = new List<Telefone>();
            while (dr.Read())
            {
                Telefone registro = ConverterParaTipoClasse(dr);
                registros.Add(registro);
            }
            dr.Close();
            comando.Dispose();

            return registros;
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

    }
}