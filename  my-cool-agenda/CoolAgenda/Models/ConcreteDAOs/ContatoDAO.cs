﻿using CoolAgenda.Models;
using CoolAgenda.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class ContatoDAO : IContatoDAO
    {
        private string SQL;

        private ITelefoneDAO telefoneDAO;

        public ContatoDAO()
        {
            telefoneDAO = new TelefoneDAO();
        }

        //Insert
        public void Insert(Contato contato)
        {
            string end = contato.Endereco;
            if (contato.Endereco == null)
                end = "";            

            SQL = "INSERT INTO Contato (IdContato, IdUsuario, Nome, Apelido, Email, Endereco, DataNascimento) VALUES (SeqContato.NEXTVAL, ?, ?, ?, ?, ?, ?)";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = SQL;

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.VarChar);
            pIdUsuario.Value = contato.IdUsuario;
            comando.Parameters.Add(pIdUsuario);           

            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = contato.Nome;
            comando.Parameters.Add(pNome);

            OleDbParameter pApelido = new OleDbParameter("Apelido", OleDbType.VarChar);
            pApelido.Value = contato.Apelido;
            comando.Parameters.Add(pApelido);

            OleDbParameter pEmail = new OleDbParameter("Email", OleDbType.VarChar);
            pEmail.Value = contato.Email;
            comando.Parameters.Add(pEmail);

            OleDbParameter pEndereco = new OleDbParameter("Endereco", OleDbType.VarChar);
            pEndereco.Value = end;
            comando.Parameters.Add(pEndereco);

            OleDbParameter pDataNascimento = new OleDbParameter("DataNascimento", OleDbType.Date);
            pDataNascimento.Value = contato.DataNascimento;
            comando.Parameters.Add(pDataNascimento);

            comando.ExecuteNonQuery();

            comando.CommandText = "";
            comando.Dispose();
        }

        //Select
        public List<Contato> Select()
        {
            SQL = "Select * From Contato Order By IdContato";

            OleDbCommand Select = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);                                                 

            OleDbDataReader dr = Select.ExecuteReader();

            List<Contato> registros = new List<Contato>();

            while (dr.Read())
            {
                Contato registro = ConverterParaTipoClasse(dr);
                registros.Add(registro);
            }

            //foreach (var registro in registros)
            //{
            //    CarregarComposicao(registro);
            //}
            return registros;
        }


        //Update
        public void Update(Contato contato)
        {
            SQL = "UPDATE Contato SET Nome = ?, Apelido = ?, Email = ?, Endereco = ?, DataNascimento = ? WHERE IdContato = ? and IdUsuario = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = SQL;

            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = contato.Nome;
            comando.Parameters.Add(pNome);

            OleDbParameter pApelido = new OleDbParameter("Apelido", OleDbType.VarChar);
            pApelido.Value = contato.Apelido;
            if (pApelido.Value == null) { pApelido.Value = ""; }
            comando.Parameters.Add(pApelido);

            OleDbParameter pEmail = new OleDbParameter("Email", OleDbType.VarChar);
            pEmail.Value = contato.Email;
            comando.Parameters.Add(pEmail);

            OleDbParameter pEndereco = new OleDbParameter("Endereco", OleDbType.VarChar);
            pEndereco.Value = contato.Endereco;
            comando.Parameters.Add(pEndereco);

            OleDbParameter pDataNascimento = new OleDbParameter("DataNascimento", OleDbType.Date);
            pDataNascimento.Value = contato.DataNascimento;
            comando.Parameters.Add(pDataNascimento);

            OleDbParameter pIdContato = new OleDbParameter("IdContato", OleDbType.Integer);
            pIdContato.Value = contato.IdContato;
            comando.Parameters.Add(pIdContato);

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.Integer);
            pIdUsuario.Value = contato.IdUsuario;
            comando.Parameters.Add(pIdUsuario);

            // Update
            comando.ExecuteNonQuery();
            comando.Dispose();
        }


        //Inativar
        public void InativarContato(int id)
        {
            SQL = "UPDATE Contato SET Ativo = 'N' WHERE IdContato = " + id;

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao() as OleDbConnection;
            comando.CommandText = SQL;

            OleDbParameter pId = new OleDbParameter("IdContato", OleDbType.VarChar);
            pId.Value = id;
            comando.Parameters.Add(pId);

            // Delete
            comando.ExecuteNonQuery();
            comando.Dispose();
        }

        public Contato BuscarPorId(int id)
        {
            Contato registro = null;
            string sqlBuscar = "select * from Contato where IdContato = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlBuscar;

            OleDbParameter pId = new OleDbParameter("IdContato", OleDbType.Integer);
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

        public List<Contato> BuscarPorIdUsuario(int id)
        {
            List<Contato> registros = new List<Contato>();
            string sqlBuscar = "select * from Contato where IdUsuario = ? and Ativo = 'S' Order By IdContato";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlBuscar;

            OleDbParameter pId = new OleDbParameter("IdUsuario", OleDbType.Integer);
            pId.Value = id;
            comando.Parameters.Add(pId);

            // Select
            OleDbDataReader dr = comando.ExecuteReader();
            while (dr.Read())
            {
                Contato registro = ConverterParaTipoClasse(dr);
                registros.Add(registro);
            }
            dr.Close();
            comando.Dispose();

            return registros;
        }



        //Conversão
        public Contato ConverterParaTipoClasse(OleDbDataReader dr)
        {
            Contato contato = new Contato();
            contato.IdContato = int.Parse(dr["IdContato"].ToString());
            contato.IdUsuario = int.Parse(dr["Idusuario"].ToString());
            contato.Nome = dr["Nome"].ToString();
            contato.Apelido = dr["Apelido"].ToString();
            contato.Email = dr["Email"].ToString();
            contato.Endereco = dr["Endereco"].ToString();
            contato.DataNascimento = DateTime.Parse(dr["DataNascimento"].ToString());
            contato.Ativo = dr["Ativo"].ToString();

            return contato;
        }

        public List<Contato> ListarContatosUsuario(int idUser, string q)
        {
            String sqlConsulta = "select * from Contato where IdUsuario = ? and Nome like  '%' || ? || '%' order by Nome";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = "alter session set nls_sort=BINARY_CI";
            comando.ExecuteNonQuery();
            comando.CommandText = "alter session set nls_comp=LINGUISTIC";
            comando.ExecuteNonQuery();
            comando.CommandText = sqlConsulta;

            OleDbParameter pIdUsuario = new OleDbParameter("IdGrupo", OleDbType.Integer);
            pIdUsuario.Value = idUser;
            comando.Parameters.Add(pIdUsuario);

            OleDbParameter pQ = new OleDbParameter("Nome", OleDbType.VarChar);
            pQ.Value = q;
            comando.Parameters.Add(pQ);

            // Select
            OleDbDataReader dr = comando.ExecuteReader();

            List<Contato> registros = new List<Contato>();
            while (dr.Read())
            {
                Contato registro = ConverterParaTipoClasse(dr);
                registros.Add(registro);
            }
            dr.Close();
            comando.Dispose();

            return registros;
        }
    }
}