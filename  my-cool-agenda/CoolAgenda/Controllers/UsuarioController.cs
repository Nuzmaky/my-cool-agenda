using CoolAgenda.Filters;
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
    [FiltroAutenticacao("U")]
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/
        private IUsuarioService usuarioService;

        // Instancia o Service usuario
        public UsuarioController()
        {
            usuarioService = new UsuarioService();
        }

        //Objetos
        Usuario usuario = new Usuario();
        UsuarioDAO usuarioDAO = new UsuarioDAO();


        // Cadastro de Usuário
        public ActionResult Index(UsuarioVM userVM)
        {
            return View(userVM);
        }


        // View de retorno de cadastro.
        [HttpPost]
        public ActionResult Form(Usuario usuario, UsuarioVM usuarioVM)
        {
            // Pega o usuário na sessão
            Usuario usuarioSession = Session["Usuario"] as Usuario;
            int idUsuario = usuarioSession.IdUsuario;


            //SE FOR USUARIO, CADASTRO COM NIVEL DE USUARIO            
            if (usuarioVM.Nivel)
                usuario.Nivel = "A"; // Tenho que ajustar ainda
            else
                usuario.Nivel = "U";

            // Insere no Banco
            usuarioDAO.Adicionar(usuario);

            // Envial E-mail de Confirmação
            UsuarioService.EnviaEmailCadastro(usuarioVM.Email, usuarioVM.Senha, usuarioVM.Nome);

            //Lista os usuários cadastrados
            usuarioVM.ListaUsuario = usuarioDAO.Listar();
            return View(usuarioVM);
        }

        // Cadastro Inativos
        [FiltroAutenticacao]
        public ActionResult CadastroInativo()
        {
            ViewBag.Inativo = "Seu cadastro está inativo. Ative clicando no link enviado por e-mail na hora de seu cadastro.";
            return View();
        }

        [PermitirAnonimos]
        public ActionResult AtivaCadastro()
        {
            return View();
        }

        [PermitirAnonimos]
        [HttpPost]
        public ActionResult AtivaCadastro(UsuarioVM vm)
        {
            if (ModelState.IsValid)
            {
                string email = vm.Email;
                string senha = vm.Senha;

                // Verifica o e-mail
                Usuario u = usuarioService.BuscarPorEmail(vm.Email);

                // Valida E-mail e Senha
                var erros = usuarioService.ValidaUsuario(email, senha);

                // Se e-mail e senha estiverem corretos
                if (erros.Count == 0)
                {
                    //Recebe Status Ativo ou nao
                    string ativo = u.Ativo;

                    //Verifica se o cadastro está ativo(S), e mostra a Agenda.
                    if (Autenticacao.VerificaCadastroAtivo(ativo))
                        return RedirectToAction("Index", "Agenda");
                    else
                    {
                        bool ativado = usuarioService.AtivarCadastro(email);
                        if (ativado)
                        {
                            ViewBag.Mensagem = "Usuário ativado com sucesso!";
                            return View();
                        }                                                 
                    }
                }                
            }
            ViewBag.Mensagem = "Não é possível ativar o cadastro. Verifique seus dados ou contate a área de suporte.";
            return View();
        }
 

        [PermitirAnonimos]
        public ActionResult DadosAtivacaoCadastro(UsuarioVM vm, Usuario usuario)
        {
            usuario.clicouAtivacao = vm.clicouAtivacao;
            return View(vm);
        }

    }
}
