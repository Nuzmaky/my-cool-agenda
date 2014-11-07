using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class UsuarioDAO : IUsuarioDAO
    {
        private string SQL;

        //Insert
        public void Insert(Usuario user)
        {
            SQL = "INSERT INTO Usuario (IdUsuario, Email, Nome, Senha, Nivel) VALUES (SeqUsuario.NEXTVAL, ?, ?, ?, ?)";

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

            OleDbParameter pNivel = new OleDbParameter("Nivel", OleDbType.VarChar);
            pNivel.Value = user.Nivel;    

            comando.Parameters.Add(pNivel);

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


        //Update
        public void Update(Usuario usuario, DbTransaction transacao)
        {
            SQL = "UPDATE Usuario SET Email = ?, Nome = ?, Senha = ?, Nivel = ? WHERE IdUsuario = ?";           

            // Configura o comando
            OleDbCommand comando = new OleDbCommand();
            comando.Connection = Conexao.getConexao();
            comando.CommandText = SQL;
            if (transacao != null)
                comando.Transaction = transacao as OleDbTransaction;

            OleDbParameter pIdUsuario = new OleDbParameter("IdUsuario", OleDbType.Integer);
            pIdUsuario.Value = usuario.IdUsuario;
            comando.Parameters.Add(pIdUsuario);


            OleDbParameter pEmail = new OleDbParameter("Email", OleDbType.VarChar);
            pEmail.Value = usuario.Email;
            comando.Parameters.Add(pEmail); 
            
            OleDbParameter pNome = new OleDbParameter("Nome", OleDbType.VarChar);
            pNome.Value = usuario.Nome;
            comando.Parameters.Add(pNome);

            OleDbParameter pSenha = new OleDbParameter("Senha", OleDbType.VarChar);
            pSenha.Value = usuario.Senha;
            comando.Parameters.Add(pSenha);

            OleDbParameter pNivel = new OleDbParameter("Nivel", OleDbType.VarChar);
            pNivel.Value = usuario.Nivel;
            comando.Parameters.Add(pNivel);

            // Update
            comando.ExecuteNonQuery();
            comando.Dispose();
        }


        //Conversão
        public Usuario ConverterParaTipoClasse(OleDbDataReader dr)
        {

            Usuario user = new Usuario();

            user.IdUsuario = int.Parse(dr["Idusuario"].ToString());
            user.Email = dr["Email"].ToString();
            user.Nome = dr["Nome"].ToString();
            user.Senha = dr["Senha"].ToString();
            user.Nivel = dr["Nivel"].ToString();

            return user;
        }


    }
}