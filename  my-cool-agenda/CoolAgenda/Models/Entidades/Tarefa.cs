using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public class Tarefa
    {
        private int idTarefa;
        private int idCompromisso; // FK
        private int idUsuario; // FK
        private string nome;
        private string descricao;
        private DateTime dataInicial;
        private DateTime dataFinal;

        
        //ID
        public int IdTarefa
        {
            get { return idTarefa; }
            set
            {
                if (idTarefa > 0) idTarefa = value;
            }
        }


        //NOME
        public string Nome
        {
            get { return nome; }
            set
            {
                if (nome != null && value.Length < 50)
                    nome = value;
            }
        }

        //DESCRIÇÃO
        public string Descricao
        {
            get { return descricao; }
            set
            {
                if (value.Length < 100)
                    descricao = value;
            }
        }

        //ID COMPROMISSO _ FK
        public int IdCompromisso
        {
            get { return idCompromisso; }
            set { idCompromisso = value; }
        }


        // ID USUARIO _ FK
        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }



        //DATA INICIAL
        public DateTime DataInicial
        {
            get { return dataInicial; }
            set { value = dataInicial; }
        }

        //DATA FINAL
        public DateTime DataFinal
        {
            get { return dataFinal; }
            set { value = dataFinal; }
        }




    }
}