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
            string sqlAdicionar = "INSERT INTO Compromisso (IdCompromisso, Nome, DataInicial, DataFinal, Cor, DiaInteiro) VALUES (SeqCompromisso.NEXTVAL, ?, ?, ?, ?, ?)";

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

        //Select
        public List<Compromisso> Listar()
        {
            String sqlConsulta = "Select * from Compromisso";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlConsulta;

            // Select
            OleDbDataReader dr = comando.ExecuteReader();

            List<Compromisso> registros = new List<Compromisso>();
            while (dr.Read())
            {
                Compromisso registro = ConverterDataReaderParaObj(dr);
                registros.Add(registro);
            }
            dr.Close();
            comando.Dispose();

            return registros;
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
    }
}