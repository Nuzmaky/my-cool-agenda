using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class CompromissoDAO : ICompromissoDAO
    {

        public void Adicionar(Compromisso entidade)
        {
            string sqlAdicionar = "INSERT INTO Compromisso (IdCompromisso, Nome, DataInicial, DataFinal) VALUES (SeqCompromisso.NEXTVAL, ?, ?, ?)";

            //configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlAdicionar;

            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = entidade.NomeCompromisso;
            comando.Parameters.Add(pNome);

            OleDbParameter pDataInicial = new OleDbParameter("DataInicial", OleDbType.Date);
            pDataInicial.Value = entidade.DataInicial;
            comando.Parameters.Add(pDataInicial);

            OleDbParameter pDataFinal = new OleDbParameter("DataFinal", OleDbType.Date);
            pDataFinal.Value = entidade.DataFinal;
            comando.Parameters.Add(pDataFinal);

            //Insert
            comando.ExecuteNonQuery();
            comando.Dispose();

        }

        public void Atualizar(Compromisso entidade)
        {
            string sqlAtualizar = "update Compromisso set Nome = ?, DataInicial = ?, DataFinal = ? where IdVoo = ?";

            //configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlAtualizar;

            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = entidade.NomeCompromisso;
            comando.Parameters.Add(pNome);

            OleDbParameter pDataInicial = new OleDbParameter("DataInicial", OleDbType.Date);
            pDataInicial.Value = entidade.DataInicial;
            comando.Parameters.Add(pDataInicial);

            OleDbParameter pDataFinal = new OleDbParameter("DataFinal", OleDbType.Date);
            pDataFinal.Value = entidade.DataFinal;
            comando.Parameters.Add(pDataFinal);

            OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.Integer);
            pIdCompromisso.Value = entidade.IdCompromisso;
            comando.Parameters.Add(pIdCompromisso);

            //Update
            comando.ExecuteNonQuery();
            comando.Dispose();
        }
    }
}