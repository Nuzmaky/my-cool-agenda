using CoolAgenda.Models;
using CoolAgenda.Models.Services;
using CoolAgenda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.Controllers
{
    public class HomeController : Controller
    {
        private UsuarioService usuarioService;

        public HomeController()
        {
            usuarioService = new UsuarioService();
        }
       
        public ActionResult Index()
        {
            return View();
        }


        //Autenticação
        [HttpPost]
        public ActionResult Index(Usuario usuario, UsuarioDAO usuarioDAO)
        {
            string email = usuario.Email;
            string senha = usuario.Senha;            

            var erros = usuarioService.ValidaUsuario(email, senha);
            if (erros.Count == 0)
            {
                Usuario u = usuarioService.AutenticaUsuario(email, senha);
                
                // Adiciona o usuário na sessão
                Session["Usuario"] = u;

                // Redireciona para o controller adequado de acordo com o nível do usuário
                string nivel = u.Nivel.ToString();

                // Redireciona para as actions padrões de acordo com o nivel do usuário
                // Se for um ADM, libera acesso
                if (Autenticacao.VerificaAdm(nivel))
                    return RedirectToAction("Index", "Usuario");
                else
                {
                    // Recebe a situação do Cadastro
                    // P - Pendente
                    // S - Ativo
                    // N - Inativo (Sem clicar no link de e-mail)
                    string ativo = u.Ativo.ToString();
                    
                    //Verifica se o cadastro está ativo(S), e mostra a Agenda.
                    if (Autenticacao.VerificaCadastroAtivo(ativo))
                        return RedirectToAction("Index", "Agenda");
                    else
                        //Verifica se o cadastro está pendente ((P)clicou no link) e ativa.
                        if (Autenticacao.VerificaCadastroPendente(ativo))
                            return RedirectToAction("AtivaCadastro", "Usuario");
                        else
                        // Exibe que o cadastro está Inativo(N).
                        return RedirectToAction("CadastroInativo", "Usuario");
                }
            }
            
            return View();
        }


        // Sair
        public ActionResult Sair()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }        
    }
}
