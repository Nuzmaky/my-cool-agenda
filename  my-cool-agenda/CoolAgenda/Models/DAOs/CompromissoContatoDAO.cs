using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class CompromissoContatoDAO : ICompromissoContatoDAO
    {
        private ICompromissoDAO compromissoDAO;
        private IContatoDAO contatoDAO;

        public CompromissoContatoDAO()
        {
            compromissoDAO = new CompromissoDAO();
            contatoDAO = new ContatoDAO();
        }


        public void Adicionar(CompromissoContato entidade, DbTransaction transaction = null)
        {
            string sqlAdicionar = "INSERT INTO CompromissoContato (IdContato, IdCompromisso, Aceito) VALUES (?, ?, ?)";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand(sqlAdicionar, Conexao.getConexao() as OleDbConnection);
            if (transaction != null)
                comando.Transaction = transaction as OleDbTransaction;

            OleDbParameter pIdContato = new OleDbParameter("IdContato", OleDbType.Integer);
            pIdContato.Value = entidade.IdContato;
            comando.Parameters.Add(pIdContato);

            OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.Integer);
            pIdCompromisso.Value = entidade.IdCompromisso;
            comando.Parameters.Add(pIdCompromisso);

            OleDbParameter pAceito = new OleDbParameter("Aceito", OleDbType.VarChar);
            pAceito.Value = entidade.Aceito;
            comando.Parameters.Add(pAceito);

            //Insert
            comando.ExecuteNonQuery();
            comando.Dispose();
        }
    }
}