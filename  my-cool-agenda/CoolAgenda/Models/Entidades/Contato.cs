using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class Contato
    {

        private int idContato;
        private int idUsuario; // FK
        private string nome;
        private string email;
        private string endereco;
        private string ativo;

        // Composição de classes
        public List<Telefone> Telefones { get; set; }
        
        // Ativo
        public string AtivoFormatada
        {
            get { return Ativo.Equals("S") ? "Sim" : "Não"; }
        }


        //ID CONTATO
        public int IdContato
        {
            get { return idContato; }
            set
            {
                if (value > 0)
                    idContato = value;
            }
        }


        // ID USUARIO _ FK
        public int IdUsuario
        {
            get { return idUsuario; }
            set
            {
                if (value > 0)
                    idUsuario = value;
            }
        }


        //NOME
        public string Nome
        {
            get { return nome; }
            set
            {
                if (value != null && value.Length < 100)
                    nome = value;
            }
        }

        //EMAIL
        public string Email
        {
            get { return email; }
            set
            {
                if (value != null && value.Length < 100)
                email = value;
            }
        }


        //ENDEREÇO
        public string Endereco
        {
            get { return endereco; }
            set
            {
                if (value != null && value.Length < 100)
                    endereco = value;
            }
        }

        //ATIVO
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