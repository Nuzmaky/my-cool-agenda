using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class GrupoUsuario
    {
        private string administrador;
        private string ativo;
        
        //chave estrangeira
        private int idGrupo;
        private int idUsuario;

        //composicao de classes
        public Grupo Grupo { get; set; }
        public Usuario Usuario { get; set; }

        public string AtivoFormatada
        {
            get { return Ativo.Equals("S") ? "Sim" : "Não"; }
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
        public int IdUsuario
        {
            get { return idUsuario; }
            set
            {
                if (value > 0)
                    idUsuario = value;
            }

        }

        public string Administrador
        {
            get { return administrador; }
            set
            {
                    administrador = value;
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