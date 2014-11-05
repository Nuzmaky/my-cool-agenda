using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class ContatoDAO
    {
        private string SQL;

        //Insert
        public void Insert(Contato contato)
        {
            SQL = "INSERT INTO Contato (IdContato, IdUsuario, Nome, Email, Endereco) VALUES (SeqContato.NEXTVAL, ?, ?, ?, ?)";

            OleDbCommand comando = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.VarChar);
            pIdUsuario.Value = contato.IdUsuario;
            comando.Parameters.Add(pIdUsuario);

            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = contato.Nome;
            comando.Parameters.Add(pNome);

            OleDbParameter pEmail = new OleDbParameter("Email", OleDbType.VarChar);
            pEmail.Value = contato.Email;
            comando.Parameters.Add(pEmail);

            OleDbParameter pEndereco = new OleDbParameter("Endereco", OleDbType.VarChar);
            pEndereco.Value = contato.Endereco;
            comando.Parameters.Add(pEndereco);

            comando.ExecuteNonQuery();
            comando.Dispose();
        }

        //Select
        public List<Contato> Select()
        {
            List<Contato> ListaContato = new List<Contato>();
            string SQL = "Select * From Contato";
            OleDbCommand Select = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            OleDbDataReader dr = Select.ExecuteReader();

            while (dr.Read())
            {
                ListaContato.Add(ConverterParaTipoClasse(dr));
            }

            return ListaContato;
        }


        //Update
        public void Update(Contato contato, DbTransaction transacao)
        {
            SQL = "UPDATE Contato SET IdContado = ?, IdUsuario = ?, Nome = ?, Email = ?, Endereco = ? WHERE IdContato = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = SQL;
            if (transacao != null)
                comando.Transaction = transacao as OleDbTransaction;

            OleDbParameter pIdContato = new OleDbParameter("IdContato", OleDbType.Integer);
            pIdContato.Value = contato.IdContato;
            comando.Parameters.Add(pIdContato);

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.Integer);
            pIdUsuario.Value = contato.IdUsuario;
            comando.Parameters.Add(pIdUsuario);

            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = contato.Nome;
            comando.Parameters.Add(pNome);

            OleDbParameter pEmail = new OleDbParameter("Email", OleDbType.VarChar);
            pEmail.Value = contato.Email;
            comando.Parameters.Add(pEmail);

            OleDbParameter pEndereco = new OleDbParameter("Endereco", OleDbType.VarChar);
            pEndereco.Value = contato.Endereco;
            comando.Parameters.Add(pEndereco);

            // Update
            comando.ExecuteNonQuery();
            comando.Dispose();
        }



        //Delete
        public void DeleteById(int id, DbTransaction transacao)
        {
            SQL = "DELETE Contato WHERE IdContato = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao() as OleDbConnection;
            comando.CommandText = SQL;
            if (transacao != null)
                comando.Transaction = transacao as OleDbTransaction;

            OleDbParameter pId = new OleDbParameter("IdContato", OleDbType.VarChar);
            pId.Value = id;
            comando.Parameters.Add(pId);

            // Delete
            comando.ExecuteNonQuery();
            comando.Dispose();
        }


        //Conversão
        public Contato ConverterParaTipoClasse(OleDbDataReader dr)
        {
            Contato contato = new Contato();
            contato.IdContato = int.Parse(dr["IdContato"].ToString());
            contato.IdUsuario = int.Parse(dr["Idusuario"].ToString());
            contato.Nome = dr["Nome"].ToString(); 
            contato.Email = dr["Email"].ToString();
            contato.Endereco = dr["Endereco"].ToString();

            return contato;
        }






    }
}