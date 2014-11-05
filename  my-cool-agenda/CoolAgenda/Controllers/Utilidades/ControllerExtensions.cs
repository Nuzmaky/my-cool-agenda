using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.Controllers.Utilidades
{
    public static class ControllerExtensions
    {
        /**
     *  Técnica de Extension Method.
     *  http://csharpbrasil.com.br/csharp/extension-methods/
     *  A ideia é estender as funcionalidades de ModelStateDictionary sem precisar criar uma herança.
     *  A vantagem é poder usar a mesma classe 
     *  ModelStateDictionary sem precisar alterar as configurações nativas do controller.
     */
        public static void AddModelErrors(this ModelStateDictionary modelState, List<Validacao> erros)
        {
            if (erros != null)
            {
                foreach (var item in erros)
                {
                    modelState.AddModelError(item.Nome, item.Mensagem);
                }
            }
        }
    }
}