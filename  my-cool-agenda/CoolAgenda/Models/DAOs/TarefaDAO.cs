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

        //Insert
        public void Adcionar(Tarefa tarefa)
        {
            SQL = "INSERT into TAREFA (idTarefa, IdCompromisso, idUsuario, Nome, Descricao, dataInicial, datafinal, ativo) VALUES (SeqTarefa.NEXTVAL, 0, 2, ?, ?, ?, ?,'S')";

            OleDbCommand comando = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            //OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.VarChar);
            //if (tarefa.IdCompromisso == null) { tarefa.IdCompromisso = 0; }
            //pIdCompromisso.Value = tarefa.IdCompromisso;
            //comando.Parameters.Add(pIdCompromisso);

            //OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.VarChar);
            //if (tarefa.IdUsuario == null) { tarefa.IdUsuario = 0; }
            //pIdUsuario.Value = tarefa.IdUsuario;
            //comando.Parameters.Add(pIdUsuario);

            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = tarefa.NomeTarefa;
            comando.Parameters.Add(pNome);


            OleDbParameter pDescricao = new OleDbParameter("Descricao", OleDbType.VarChar);
            pDescricao.Value = tarefa.DescTarefa;
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
        public List<Tarefa> Listar()
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
            SQL = "UPDATE Tarefa SET idTarefa, IdCompromisso = ?, idUsuario = ?, Nome = ?, Descricao = ?, dataInicial = ?, datafinal = ? ativo = ? WHERE IdTarefa = ?";

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
            pNome.Value = tarefa.NomeTarefa;
            comando.Parameters.Add(pNome);


            OleDbParameter pDescricao = new OleDbParameter("Descricao", OleDbType.VarChar);
            pDescricao.Value = tarefa.DescTarefa;
            comando.Parameters.Add(pDescricao);

            OleDbParameter pDataInicial = new OleDbParameter("DataInicial", OleDbType.VarChar);
            pDataInicial.Value = tarefa.DataInicial;
            comando.Parameters.Add(pDataInicial);

            OleDbParameter pDataFinal = new OleDbParameter("DataFinal", OleDbType.VarChar);
            pDataInicial.Value = tarefa.DataFinal;
            comando.Parameters.Add(pDataFinal);

            OleDbParameter pAtivo = new OleDbParameter("Ativo", OleDbType.VarChar);
            pDataInicial.Value = tarefa.Ativo;
            comando.Parameters.Add(pAtivo);


            // Update
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
            tarefa.NomeTarefa = dr["Nome"].ToString();
            tarefa.DescTarefa = dr["Descricao"].ToString();
            tarefa.DataInicial = dr["dataInicial"].ToString();
            tarefa.DataFinal = dr["dataFinal"].ToString();

            return tarefa;
        }
        
            public List<Tarefa> populaTarefas(Tarefa tarefa)
        {
            List<Tarefa> listaTarefa = new List<Tarefa>();
            string SQL = "Select C.Idtarefa, C.nomeTarefa from Tarefa C, where C.idtarefa = ?;";
            OleDbCommand Select = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);
            
            OleDbParameter pidusuario = new OleDbParameter("Descricao", OleDbType.VarChar);
            pidusuario.Value = tarefa.IdUsuario;
            Select.Parameters.Add(pidusuario);

            OleDbDataReader dr = Select.ExecuteReader();

            while (dr.Read())
            {
                listaTarefa.Add(ConverteCompTare(dr));
            }

            return listaTarefa;
        }

        public Tarefa ConverteCompTare(OleDbDataReader dr)
        {
            Tarefa tarefa = new Tarefa();
            tarefa.IdCompromisso = int.Parse(dr["IdCompromisso"].ToString());
            tarefa.NomeTarefa = dr["nome"].ToString();

            return tarefa;
        }

        public Tarefa BuscarPorId(int id)
        {
            Tarefa registro = null;
            string sqlBuscar = "select * from Tarefa where IdTarefa = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlBuscar;

            OleDbParameter pId = new OleDbParameter("IdTarefa", OleDbType.Integer);
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
        


    }
}