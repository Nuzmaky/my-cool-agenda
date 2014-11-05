using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoolAgenda.Controllers.Utilidades
{
    public class JsonActionResultModel
    {

        public bool Erro { get; set; }
        public string Mensagem { get; set; }


        public JsonActionResultModel(string msg)
        {
            Mensagem = msg;
        }

        public JsonActionResultModel()
        { }

        public void AddValidationResultErros(List<Validacao> erros)
        {
            if (erros != null)
            {
                foreach (var item in erros)
                {
                    Mensagem += item.Mensagem + "\n";
                }
            }
        }

    }
}