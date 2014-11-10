using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models.Services
{
    public class Autenticacao
    {
        //Verifica se é um Administrador
        public static bool VerificaAdm(string nivel)
        {
            return nivel.Equals("A", StringComparison.InvariantCultureIgnoreCase);
        }

        //Verifica se o Cadastro está ativo
        public static bool VerificaCadastroAtivo(string ativo)
        {
            return ativo.Equals("S",StringComparison.InvariantCultureIgnoreCase);
        }

        //Verifica se o Cadastro está pendente
        public static bool VerificaCadastroPendente(string ativo)
        {
            return ativo.Equals("P", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}