using CoolAgenda.Filters;
using CoolAgenda.Models;
using CoolAgenda.ViewModels;   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoolAgenda.Controllers
{
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
        [FiltroAutenticacao("A")]
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

        
        public ActionResult AtivaCadastro(Usuario usuario)
        {
            // Pega o usuário na sessão
            Usuario usuarioSession = Session["Usuario"] as Usuario;
            string email = usuarioSession.Email;
                        
            bool ativo = usuarioService.AtivarCadastro(email);
            if (ativo)
            {
                ViewBag.Sucesso = "Usuário ativado com sucesso!";
                return View();
            }
            else
                ViewBag.Mensagem = "Não é possível ativar o registro solicitado. Contate a área de suporte.";
            
            return View(usuario);            
        }

        
        public ActionResult DadosAtivacaoCadastro(UsuarioVM vm, Usuario usuario)
        {
            usuario.clicouAtivacao = vm.clicouAtivacao;
            return View(vm);
        }

    }
}
