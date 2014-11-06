using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class TarefaDAO
    {
        private string SQL;
        private int idUsuario;

        //Insert
        public void Insert(Tarefa tarefa)
        {
            SQL = "INSERT into TAREFA (idTarefa, IdCompromisso, idUsuario, Nome, Descricao, dataInicial, datafinal) VALUES (SeqContato.NEXTVAL, ?, ?, ?, ?, ?, ?, ?)";

            OleDbCommand comando = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            OleDbParameter pIdTarefa = new OleDbParameter("IdTarefa", OleDbType.VarChar);
            pIdTarefa.Value = tarefa.IdTarefa;
            comando.Parameters.Add(pIdTarefa);
            //int user = Convert.ToInt32(Session["Usuario]);

            OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.VarChar);
            pIdCompromisso.Value = tarefa.IdCompromisso;
            comando.Parameters.Add(pIdCompromisso);

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.VarChar);
            pIdUsuario.Value = tarefa.IdUsuario;
            comando.Parameters.Add(pIdUsuario);

            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = tarefa.Nome;
            comando.Parameters.Add(pNome);


            OleDbParameter pDescricao = new OleDbParameter("Descricao", OleDbType.VarChar);
            pDescricao.Value = tarefa.Descricao;
            comando.Parameters.Add(pDescricao);

            OleDbParameter pDataInicial = new OleDbParameter("DataInicial", OleDbType.VarChar);
            pDataInicial.Value = tarefa.DataInicial;
            comando.Parameters.Add(pDataInicial);

            OleDbParameter pDataFinal = new OleDbParameter("DataFinal", OleDbType.VarChar);
            pDataInicial.Value = tarefa.DataFinal;
            comando.Parameters.Add(pDataFinal);

            comando.ExecuteNonQuery();
            comando.Dispose();
        }

        //Select
        public List<Tarefa> Select()
        {
            List<Tarefa> ListaTarefa = new List<Tarefa>();
            string SQL = "Select * From Tarefa";
            OleDbCommand Select = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            OleDbDataReader dr = Select.ExecuteReader();

            while (dr.Read())
            {
                ListaTarefa.Add(ConverterParaTipoClasse(dr));
            }

            return ListaTarefa;
        }


        //Update
        public void Update(Tarefa tarefa, DbTransaction transacao)
        {
            SQL = "UPDATE Tarefa SET idTarefa, IdCompromisso = ?, idUsuario = ?, Nome = ?, Descricao = ?, dataInicial = ?, datafinal = ? WHERE IdTarefa = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = SQL;
            if (transacao != null)
                comando.Transaction = transacao as OleDbTransaction;

            OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.VarChar);
            pIdCompromisso.Value = tarefa.IdCompromisso;
            comando.Parameters.Add(pIdCompromisso);

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.VarChar);
            pIdUsuario.Value = tarefa.IdUsuario;
            comando.Parameters.Add(pIdUsuario);

            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = tarefa.Nome;
            comando.Parameters.Add(pNome);


            OleDbParameter pDescricao = new OleDbParameter("Descricao", OleDbType.VarChar);
            pDescricao.Value = tarefa.Descricao;
            comando.Parameters.Add(pDescricao);

            OleDbParameter pDataInicial = new OleDbParameter("DataInicial", OleDbType.VarChar);
            pDataInicial.Value = tarefa.DataInicial;
            comando.Parameters.Add(pDataInicial);

            OleDbParameter pDataFinal = new OleDbParameter("DataFinal", OleDbType.VarChar);
            pDataInicial.Value = tarefa.DataFinal;
            comando.Parameters.Add(pDataFinal);


            // Update
            comando.ExecuteNonQuery();
            comando.Dispose();
        }



        //Delete
        public void DeleteById(int id, DbTransaction transacao)
        {
            SQL = "DELETE tarefa WHERE IdTarefa = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao() as OleDbConnection;
            comando.CommandText = SQL;
            if (transacao != null)
                comando.Transaction = transacao as OleDbTransaction;

            OleDbParameter pId = new OleDbParameter("IdTarefa", OleDbType.VarChar);
            pId.Value = id;
            comando.Parameters.Add(pId);

            // Delete
            comando.ExecuteNonQuery();
            comando.Dispose();
        }


        //Conversão
        public Tarefa ConverterParaTipoClasse(OleDbDataReader dr)
        {
            Tarefa tarefa = new Tarefa();
            tarefa.IdTarefa = int.Parse(dr["IdTarefa"].ToString());
            tarefa.IdCompromisso = int.Parse(dr["IdCompromisso"].ToString());
            tarefa.IdUsuario = int.Parse(dr["Idusuario"].ToString()); dr["IdUsuario"].ToString();
            tarefa.Nome = dr["Nome"].ToString();
            tarefa.Descricao = dr["Descrição"].ToString();
            tarefa.DataInicial = (DateTime) dr["dataInicial"];
            tarefa.DataFinal = (DateTime) dr["dataFinal"];

            return tarefa ;
        }

        public List<Compromisso> populaCompromissos(Tarefa tarefa)
        {
            List<Compromisso> listaCompromisso = new List<Compromisso>();
            string SQL = "Select NOME From Compromisso Where ID";
            OleDbCommand Select = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            OleDbDataReader dr = Select.ExecuteReader();

            while (dr.Read())
            {
                listaCompromisso.Add(ConverterParaTipoClasse(dr);
            }

            return listaCompromisso;
        }

        public Compromisso ConverterParaTipoClasse(OleDbDataReader dr)
        {
            Compromisso compromisso = new Compromisso();
            compromisso.IdCompromisso = int.Parse(dr["IdCompromisso"].ToString());

            return compromisso;
        }



    }
}