using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class TarefaDAO : ITarefaDAO
    {
        private string SQL;

        //Insert
        public void Adcionar(Tarefa tarefa)
        {
            SQL = "INSERT into TAREFA (idTarefa, idCompromisso, idUsuario, Nome, Descricao, DataInicial, DataFinal) VALUES (SeqTarefa.NEXTVAL, ?, ?, ?, ?, TO_DATE(?, 'DD-MM-YY HH24:MI'), TO_DATE(?, 'DD-MM-YY HH24:MI'))";

            OleDbCommand comando = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);
            
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
            pDataFinal.Value = tarefa.DataFinal;
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
        public void Update(Tarefa tarefa)
        {
            SQL = "UPDATE Tarefa SET idUsuario = ?, Nome = ?, Descricao = ?,  DataInicial = TO_DATE(?, 'DD-MM-YY HH24:MI'), DataFinal = TO_DATE(?, 'DD-MM-YY HH24:MI') WHERE IdTarefa = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = SQL;
           
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
            pDataFinal.Value = tarefa.DataFinal;
            comando.Parameters.Add(pDataFinal);

            OleDbParameter pidTarefa = new OleDbParameter("IdTarefa", OleDbType.VarChar);
            pidTarefa.Value = tarefa.IdTarefa;
            comando.Parameters.Add(pidTarefa);

            // Update
            comando.ExecuteNonQuery();
            comando.Dispose();
        }

        //Conversão
        public Tarefa ConverterParaTipoClasse(OleDbDataReader dr)
        {
            Tarefa tarefa = new Tarefa();
            tarefa.IdTarefa = int.Parse(dr["IdTarefa"].ToString());
            int aux;
            tarefa.NomeTarefa = dr["NOME"].ToString();
            tarefa.IdCompromisso = int.TryParse(dr["IdCompromisso"].ToString(), out aux) ? aux : (int?)null;
            tarefa.IdUsuario = int.Parse(dr["Idusuario"].ToString());
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

        public List<Tarefa> ListarId(int idUser)
        {
            String sqlConsulta = "Select * From Tarefa where IdUsuario = ? and Ativo = 'S' ";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlConsulta;

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.Integer);
            pIdUsuario.Value = idUser;
            comando.Parameters.Add(pIdUsuario);

            // Select
            OleDbDataReader dr = comando.ExecuteReader();

            List<Tarefa> registros = new List<Tarefa>();
            while (dr.Read())
            {
                Tarefa compromissoUser = ConverterParaTipoClasse(dr);
                registros.Add(compromissoUser);
            }
            dr.Close();
            comando.Dispose();


            return registros;
        }
        


    }
}