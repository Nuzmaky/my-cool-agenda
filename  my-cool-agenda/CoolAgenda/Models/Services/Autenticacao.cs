using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models.Services
{
    public class Autenticacao
    {
        public static bool VerificaAdm(string nivel)
        {
            return nivel.Equals("A", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}