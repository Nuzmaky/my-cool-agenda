using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class Grupo
    {
        private int idGrupo;
        private string nome;
        private string cnpj;
        private string flagAtivo;

        public string FlagAtivoFormatada
        {
            get { return flagAtivo.Equals("S") ? "Sim" : "Não"; }
        }

        // ID GRUPO
        public int IdGrupo
        {
            get { return idGrupo; }
            set
            {
                if (value > 0)
                    idGrupo = value;
            }
        }

        //NOME 
        public string Nome
        {   
            get { return nome; }
            set
            {
                if (value != null && value.Length <= 50)
                    nome = value;
            }

        }

        public string CNPJ
        {
            get { return cnpj; }
            set
            {
                if (value != null && value.Length <= 14)
                    cnpj = value;
            }
        }

        public string FlagAtivo
        {
            get { return flagAtivo; }
            set
            {
                if (value != null && value.Length == 1)
                {
                    string vFlagUpper = value.ToUpper();
                    if (vFlagUpper.Equals("S") || vFlagUpper.Equals("N"))
                        flagAtivo = vFlagUpper;
                }
            }

        }
    }
}