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

        public void Adicionar(Compromisso entidade, DbTransaction transaction = null)
        {
            string sqlAdicionar = "INSERT INTO Compromisso (IdCompromisso, Nome, DataInicial, DataFinal, Cor, DiaInteiro) VALUES (?, ?, ?, ?, ?, ?)";

            OleDbCommand comando = new OleDbCommand(sqlAdicionar, Conexao.getConexao() as OleDbConnection);
            if (transaction != null)
                comando.Transaction = transaction as OleDbTransaction;

            OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.Integer);
            pIdCompromisso.Value = entidade.IdCompromisso;
            comando.Parameters.Add(pIdCompromisso);

            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = entidade.NomeCompromisso;
            comando.Parameters.Add(pNome);

            OleDbParameter pDataInicial = new OleDbParameter("DataInicial", OleDbType.Date);
            pDataInicial.Value = entidade.DataInicial;
            comando.Parameters.Add(pDataInicial);

            OleDbParameter pDataFinal = new OleDbParameter("DataFinal", OleDbType.Date);
            pDataFinal.Value = entidade.DataFinal;
            comando.Parameters.Add(pDataFinal);

            OleDbParameter pCor = new OleDbParameter("Cor", OleDbType.VarChar);
            pCor.Value = entidade.Cor;
            comando.Parameters.Add(pCor);

            OleDbParameter pDiaInteirp = new OleDbParameter("DiaInteiro", OleDbType.VarChar);
            pDiaInteirp.Value = entidade.DiaInteiro;
            comando.Parameters.Add(pDiaInteirp);

            //Insert
            comando.ExecuteNonQuery();
            comando.Dispose();

        }

        public void Atualizar(Compromisso entidade)
        {
            string sqlAtualizar = "update Compromisso set Nome = ?, DataInicial = ?, DataFinal = ?, Cor = ?, DiaInteiro = ? where IdCompromisso = ?";

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

            OleDbParameter pCor = new OleDbParameter("Cor", OleDbType.VarChar);
            pCor.Value = entidade.Cor;
            comando.Parameters.Add(pCor);

            OleDbParameter pDiaInteirp = new OleDbParameter("DiaInteiro", OleDbType.VarChar);
            pDiaInteirp.Value = entidade.DiaInteiro;
            comando.Parameters.Add(pDiaInteirp);

            OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.Integer);
            pIdCompromisso.Value = entidade.IdCompromisso;
            comando.Parameters.Add(pIdCompromisso);

            //Update
            comando.ExecuteNonQuery();
            comando.Dispose();
        }

        public Compromisso BuscarPorId(int idCompromisso)
        {
            Compromisso registro = null;
            String sqlConsulta = "Select * from Compromisso where IdCompromisso = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlConsulta;

            OleDbParameter pId = new OleDbParameter("IdCompromisso", OleDbType.Integer);
            pId.Value = idCompromisso;
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


        private Compromisso ConverterDataReaderParaObj(OleDbDataReader dr)
        {
            Compromisso registro = new Compromisso();
            registro.IdCompromisso = Int32.Parse(dr["IdCompromisso"].ToString());
            registro.NomeCompromisso = dr["Nome"].ToString();
            registro.DataInicial = DateTime.Parse(dr["DataInicial"].ToString());
            registro.DataFinal = DateTime.Parse(dr["DataFinal"].ToString());
            registro.Ativo = dr["Ativo"].ToString();
            registro.Cor = dr["Cor"].ToString();
            registro.DiaInteiro = Boolean.Parse(dr["DiaInteiro"].ToString());

            return registro;
        }

        public int ProximoIdCompromisso(DbTransaction transaction = null)
        {
            string SQL = "select seqCompromisso.nextval from dual";

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
            throw new Exception("Erro ao recuperar o próximo ID do Compromisso.");
        }


        public void Excluir(int id)
        {
            String sqlAtualizar = "update Compromisso set Ativo = 'N' where IdCompromisso = " + id;

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