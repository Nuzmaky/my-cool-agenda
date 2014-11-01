using System.Data.OleDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models.DAOs
{
    public class Conexao
    {
        private static OleDbConnection conexao;
        private static string strConexao = "Provider=OraOleDB.Oracle;Data Source=localhost;Persist Security Info=True;User ID=piycq;Password=piycq";

        // Conexao
        public static OleDbConnection getConexao()
        {
            if (conexao == null)
                conexao = new OleDbConnection(strConexao);

            if (conexao.State == System.Data.ConnectionState.Closed)
                conexao.Open();
            return conexao;
        }
    }
}