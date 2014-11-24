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

        public List<CompromissoContato> ListarContatoDoCompromisso(int id)
        {
            String sqlConsulta = "select * from CompromissoContato where IdCompromisso = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlConsulta;

            OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.Integer);
            pIdCompromisso.Value = id;
            comando.Parameters.Add(pIdCompromisso);

            // Select

            OleDbDataReader dr = comando.ExecuteReader();

            List<CompromissoContato> registros = new List<CompromissoContato>();
            while (dr.Read())
            {
                CompromissoContato registro = ConverterDataReaderParaObj(dr);
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

        private void CarregarComposicao(CompromissoContato compromissoContato)
        {
            int idContato = compromissoContato.IdContato;
            Contato contato = contatoDAO.BuscarPorId(idContato);
            compromissoContato.Contato = contato;
        }


        private CompromissoContato ConverterDataReaderParaObj(OleDbDataReader dr)
        {
            CompromissoContato compromissoContato = new CompromissoContato();
            compromissoContato.IdContato = Int32.Parse(dr["IdContato"].ToString());
            compromissoContato.IdCompromisso = Int32.Parse(dr["IdCompromisso"].ToString());
            compromissoContato.Aceito = dr["Aceito"].ToString();

            return compromissoContato;
        }

    }
}