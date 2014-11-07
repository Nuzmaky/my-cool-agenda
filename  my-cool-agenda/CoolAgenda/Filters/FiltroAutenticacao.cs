using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace CoolAgenda.Filters
{
    public class FiltroAutenticacao : ActionFilterAttribute
    {
        // Indica o cargo(s), função(es) que o filtro deverá permitir o acesso
        public string Niveis { get; set; }

        /// <summary>
        /// Papeis:
        /// U = Usuario
        /// A = Administrador
        /// </summary>
        /// <param name="papeis"></param>
        /// 

        public FiltroAutenticacao(string papeis = "U")
        {
            Niveis = papeis;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool filtroAtivo = true;

            if (filtroAtivo)
            {
                object u = filterContext.HttpContext.Session["Usuario"];

                // Se não houver Nivel na sessão
                if (u != null)
                {
                    //Joga o Nivel
                    Usuario usuario = (Usuario)u;
                    string nivel = usuario.Nivel.ToString();

                    bool podeAcessar = VerificarAcesso(nivel, Niveis);
                    if (podeAcessar)
                        // Libera Acesso
                        base.OnActionExecuting(filterContext);
                    else
                        filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary(new { controller = "Erro", action = "Forbidden" }));
                }
                else
                {
                    // Url onde o usuário estava tentando acessar
                    string url = filterContext.HttpContext.Request.Url.PathAndQuery;

                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Home", action = "Index", url = url }));
                }
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }        

        private bool VerificarAcesso(string nivel, string Niveis)
        {
            string[] papeisAceitos = Niveis.Split(',');

            if (papeisAceitos.Contains("U", StringComparer.InvariantCultureIgnoreCase))
                return true;
            else
                return papeisAceitos.Contains(nivel);
        }
    }
}
