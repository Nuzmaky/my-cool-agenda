using CoolAgenda.Models.Entidades;
using System.Data.OleDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models.DAOs
{
    public class UsuarioDAO
    {
        private string SQL;

        //Insert
        public void Insert(Usuario user)
        {
            SQL = "INSERT INTO Usuario (IdUsuario, Email, Nome, Senha) VALUES (SeqUsuario.NEXTVAL, ?, ?, ?)";

            OleDbCommand comando = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            OleDbParameter pEmail = new OleDbParameter("Email", OleDbType.VarChar);
            pEmail.Value = user.Email;
            comando.Parameters.Add(pEmail);

            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = user.Nome;
            comando.Parameters.Add(pNome);

            OleDbParameter pSenha = new OleDbParameter("Senha", OleDbType.VarChar);
            pSenha.Value = user.Senha;
            comando.Parameters.Add(pSenha);

            comando.ExecuteNonQuery();
            comando.Dispose();
        }

       
        //Select
        public List<Usuario> Select()
        {
            List<Usuario> ListaUsuario = new List<Usuario>();
            string SQL = "Select * From Usuario";
            OleDbCommand Select = new OleDbCommand(SQL, Conexao.getConexao() as OleDbConnection);

            OleDbDataReader dr = Select.ExecuteReader();

            while (dr.Read())
            {
                ListaUsuario.Add(ConverterParaTipoClasse(dr));
            }

            return ListaUsuario;
        }


        //Conversão
        public Usuario ConverterParaTipoClasse(OleDbDataReader dr)
        {

            Usuario user = new Usuario();

            user.IdUsuario = int.Parse(dr["Idusuario"].ToString());
            user.Email = dr["Email"].ToString();
            user.Nome = dr["Nome"].ToString();
            user.Senha = dr["Senha"].ToString();

            return user;
        }


    }
}