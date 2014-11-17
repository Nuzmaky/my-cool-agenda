using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class CompromissoUsuario
    {
        private string criador;
        private string ativo;

        //chave estrangeira
        private int idGrupo;
        private int idUsuario;
        private int idCompromisso;

        //composicao de classes
        public Grupo Grupo { get; set; }
        public Usuario Usuario { get; set; }

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
        public int IdUsuario
        {
            get { return idUsuario; }
            set
            {
                if (value > 0)
                    idUsuario = value;
            }

        }

        //COMPROMISSO 
        public int IdCompromisso
        {
            get { return idCompromisso; }
            set
            {
                if (value > 0)
                    idCompromisso = value;
            }

        }

        public string Criador
        {
            get { return criador; }
            set
            {
                if (value != null && value.Length == 1)
                {
                    string vFlagUpper = value.ToUpper();
                    if (vFlagUpper.Equals("S") || vFlagUpper.Equals("N"))
                        criador = vFlagUpper;
                }
            }

        }

        public string Ativo
        {
            get { return ativo; }
            set
            {
                if (value != null && value.Length == 1)
                {
                    string vFlagUpper = value.ToUpper();
                    if (vFlagUpper.Equals("S") || vFlagUpper.Equals("N"))
                        ativo = vFlagUpper;
                }
            }

        }
    }
}