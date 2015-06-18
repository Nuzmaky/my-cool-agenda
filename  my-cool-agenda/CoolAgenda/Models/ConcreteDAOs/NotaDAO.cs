using CoolAgenda.Models;
using CoolAgenda.Models.TemplateDAOs;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class NotaDAO : TemplateNota
    {
        private string SQL;
        private IUsuarioDAO usuarioDAO;

        public NotaDAO()
        {
            usuarioDAO = new UsuarioDAO();
        }

        //Insert
        public override void Adcionar(Nota nota)
        {
            SQL = "INSERT into NOTA (idNota, idCompromisso, idUsuario, Texto, Ativo) VALUES (SeqTarefa.NEXTVAL, ?, ?, ?,'S')";

            OleDbCommand comando = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.VarChar);
            pIdCompromisso.Value = nota.IdCompromisso;
            comando.Parameters.Add(pIdCompromisso);

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
        public override List<Nota> Listar()
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

        public override List<Nota> Listar(int id)
        {
            List<Nota> ListaNota = new List<Nota>();
            string SQL = "Select * From Nota where IdCompromisso =" + id;
            OleDbCommand Select = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            OleDbDataReader dr = Select.ExecuteReader();

            while (dr.Read())
            {
                ListaNota.Add(ConverterParaTipoClasse(dr));
            }

            foreach (var registro in ListaNota)
            {
                CarregarComposicao(registro);
            }

            return ListaNota;
        }


        private void CarregarComposicao(Nota registro)
        {
            int idUsuario = registro.IdUsuario;
            Usuario user = usuarioDAO.BuscarPorId(idUsuario);
            registro.Usuario = user;

        }

        //Update
        public override void Update(Nota nota)
        {
            SQL = "UPDATE Nota SET Texto = ? WHERE idNota = ? and IdCompromisso = ? and idUsuario = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = SQL;

            OleDbParameter pTexto = new OleDbParameter("Texto", OleDbType.VarChar);
            pTexto.Value = nota.Texto;
            comando.Parameters.Add(pTexto);

            OleDbParameter pIdNota = new OleDbParameter("IdNota", OleDbType.VarChar);
            pIdNota.Value = nota.IdNota;
            comando.Parameters.Add(pIdNota);

            OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.VarChar);
            pIdCompromisso.Value = nota.IdCompromisso;
            comando.Parameters.Add(pIdCompromisso);

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.VarChar);
            pIdUsuario.Value = nota.IdUsuario;
            comando.Parameters.Add(pIdUsuario);


            // Update
            comando.ExecuteNonQuery();
            comando.Dispose();
        }

        //Conversão
        public override Nota ConverterParaTipoClasse(OleDbDataReader dr)
        {
            Nota nota = new Nota();
            nota.IdNota = int.Parse(dr["IdNota"].ToString());
            nota.IdCompromisso = int.Parse(dr["IdCompromisso"].ToString());
            nota.IdUsuario = int.Parse(dr["Idusuario"].ToString());
            nota.Texto = dr["Texto"].ToString();
            nota.Ativo = dr["Ativo"].ToString();

            return nota;
        }

        public override Nota BuscarPorId(int id)
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

        public override Nota BuscarNotaUsuarioCompromisso(int id, int idUser)
        {
            Nota registro = null;
            string sqlBuscar = "select * from Nota where IdCompromisso = ? and IdUsuario = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlBuscar;

            OleDbParameter pIdCompromisso = new OleDbParameter("IdCompromisso", OleDbType.Integer);
            pIdCompromisso.Value = id;
            comando.Parameters.Add(pIdCompromisso);

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.Integer);
            pIdUsuario.Value = idUser;
            comando.Parameters.Add(pIdUsuario);

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

        public override List<Nota> VerificarUsuarioCriador(int id, int idUser)
        {
            string sqlBuscar = "select * from Nota where IdNota = ? and IdUsuario = ?";

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = sqlBuscar;

            OleDbParameter pIdNota = new OleDbParameter("IdNota", OleDbType.Integer);
            pIdNota.Value = id;
            comando.Parameters.Add(pIdNota);

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.Integer);
            pIdUsuario.Value = idUser;
            comando.Parameters.Add(pIdUsuario);

            // Select
            OleDbDataReader dr = comando.ExecuteReader();

            List<Nota> registros = new List<Nota>();
            while (dr.Read())
            {
                Nota registro = ConverterParaTipoClasse(dr);
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
    }
}