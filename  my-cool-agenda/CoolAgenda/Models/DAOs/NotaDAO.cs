using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class NotaDAO : INotaDAO
    {
        private string SQL;

        //Insert
        public void Adcionar(Nota nota)
        {
            SQL = "INSERT into NOTA (idNota, idCompromisso, idUsuario, Texto, Ativo) VALUES (SeqTarefa.NEXTVAL, 1, ?, ?,'S')";

            OleDbCommand comando = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            //OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.VarChar);
            //pIdCompromisso.Value = nota.IdCompromisso;
            //comando.Parameters.Add(pIdCompromisso);

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.VarChar);
            pIdUsuario.Value = nota.IdUsuario;
            comando.Parameters.Add(pIdUsuario);

            OleDbParameter pTexto = new OleDbParameter("texto", OleDbType.VarChar);
            pTexto.Value = nota.Texto;
            comando.Parameters.Add(pTexto);

            comando.ExecuteNonQuery();
            comando.Dispose();
        }

        //Select
        public List<Nota> Listar()
        {
            List<Nota> ListaNota = new List<Nota>();
            string SQL = "Select * From Nota";
            OleDbCommand Select = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            OleDbDataReader dr = Select.ExecuteReader();

            while (dr.Read())
            {

                ListaNota.Add(ConverterParaTipoClasse(dr));
            }

            return ListaNota;
        }


        //Update
        public void Update(Nota nota)
        {
            SQL = "UPDATE Nota SET idNota, IdCompromisso = ?, idUsuario = ?, Texto = ? WHERE IdTarefa = ? and Ativo = 'S'";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = SQL;
            
            OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.VarChar);
            pIdCompromisso.Value = nota.IdCompromisso;
            comando.Parameters.Add(pIdCompromisso);

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.VarChar);
            pIdUsuario.Value = nota.IdUsuario;
            comando.Parameters.Add(pIdUsuario);

            OleDbParameter pTexto = new OleDbParameter("texto", OleDbType.VarChar);
            pTexto.Value = nota.Texto;
            comando.Parameters.Add(pTexto);


            // Update
            comando.ExecuteNonQuery();
            comando.Dispose();
        }

        //Conversão
        public Nota ConverterParaTipoClasse(OleDbDataReader dr)
        {
            Nota nota = new Nota();
            nota.IdNota = int.Parse(dr["IdNota"].ToString());
            nota.IdCompromisso = int.Parse(dr["IdCompromisso"].ToString());
            nota.IdUsuario = int.Parse(dr["Idusuario"].ToString());
            nota.Texto = dr["Texto"].ToString();
            nota.Ativo = dr["Ativo"].ToString();

            return nota;
        }

        public Nota BuscarPorId(int id)
        {
            Nota registro = null;
            string sqlBuscar = "select * from Nota where IdNota = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlBuscar;

            OleDbParameter pId = new OleDbParameter("IdNota", OleDbType.Integer);
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