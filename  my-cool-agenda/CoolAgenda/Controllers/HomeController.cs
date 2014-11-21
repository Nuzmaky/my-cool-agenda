using CoolAgenda.Models;
using CoolAgenda.Models.Services;
using CoolAgenda.Controllers.Utilidades;
using CoolAgenda.ViewModels.HomeVM;
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
            if (Session["Usuario"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Agenda");
            }
            
        }


        //Autenticação
        [HttpPost]
        public ActionResult Index(LoginVM vm, UsuarioDAO usuarioDAO)
        {
            if (ModelState.IsValid)
            {
                string email = vm.Email;
                string senha = vm.Senha;

                // Valida se Email e Senha estão corretos.
                var erros = usuarioService.ValidaUsuario(email, senha);

                // Se estiver corretos
                if (erros.Count == 0)
                {
                    // Valida a ativação
                    Usuario usuario = usuarioService.BuscarPorEmail(email);
                    string ativo = usuario.Ativo;

                    //Se cadastro estiver inativo, adiciona o erro
                    if (!Autenticacao.VerificaCadastroAtivo(ativo))
                        erros.Add(new Validacao("Usuário inativo. Verifique sua caixa de entrada, e clique no link de ativação"));
                    else
                    {
                        // Se não autentica
                        Usuario u = usuarioService.AutenticaUsuario(email, senha);

                        // Adiciona o usuário na sessão
                        Session["Usuario"] = u;

                        // Redireciona para o conwtroller adequado de acordo com o nível do usuário
                        string nivel = u.Nivel.ToString();

                        // Redireciona para as actions padrões de acordo com o nivel do usuário
                        // Se for um ADM, libera acesso
                        if (Autenticacao.VerificaAdm(nivel))
                            return RedirectToAction("Index", "Grupo");
                        else
                        {
                            return RedirectToAction("Index", "Agenda");
                        }
                    }
                }
                ModelState.AddModelErrors(erros);
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
