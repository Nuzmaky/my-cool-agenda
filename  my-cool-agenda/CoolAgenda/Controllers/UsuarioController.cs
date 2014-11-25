using CoolAgenda.Filters;
using CoolAgenda.Models;
using CoolAgenda.Models.Services;
using CoolAgenda.ViewModels;
using CoolAgenda.ViewModels.UserVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolAgenda.Controllers.Utilidades;

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


        [FiltroAutenticacao("A")]
        // Cadastro de Usuário
        public ActionResult Index(UsuarioVM userVM)
        {
            return View(userVM);
        }

        public ActionResult MeusDados()
        {
            MudarNome vm;

            Usuario usuarioSession = Session["Usuario"] as Usuario;
            int id = usuarioSession.IdUsuario;

            if (id > 1)
            { 
                vm = ConstruirFormVMParaEdicao(id);
                if (vm == null)
                    return new HttpNotFoundResult();
            }
            else
            {
                return new HttpNotFoundResult();
            }

            return View(vm);
        }

        [HttpPost]
        public ActionResult EditarNome(MudarNome vm)
        {
            if (ModelState.IsValid)
            {
                Usuario user = new Usuario();
                user.Nome = vm.Nome;
                user.IdUsuario = vm.IdUsuario;

                usuarioService.AtualizarNome(user);

                ViewBag.Mensagem = "Nome alterado com sucesso!";

                return View(vm);
            }
            else
            {
                ViewBag.Mensagem = "Impossível alterar o nome!";
                return View(vm);
            }

        }

        public ActionResult MudarSenha()
        {
            MeusDadosVM vm;

            Usuario usuarioSession = Session["Usuario"] as Usuario;
            int id = usuarioSession.IdUsuario;

            if (id > 1)
            {
                vm = ConstruirFormVMParaEdicaoSenha(id);
                if (vm == null)
                    return new HttpNotFoundResult();
            }
            else
            {
                return new HttpNotFoundResult();
            }
            return View(vm);
        }


        [HttpPost]
        public ActionResult EditarSenha(MeusDadosVM vm)
        {
            if (ModelState.IsValid)
            {
                Usuario user = new Usuario();
                user.Senha = vm.SenhaAtual;
                user.IdUsuario = vm.IdUsuario;

                var erros = usuarioService.ValidaSenhaAtual(user);

                if (erros.Count == 0)
                {
                    user.Senha = vm.SenhaConfirmacao;
                    usuarioService.AtualizarSenha(user);

                    ViewBag.Mensagem = "Senha alterada com sucesso!";

                    return View(vm);
                }
                else
                {
                    ModelState.AddModelErrors(erros);
                    ViewBag.Mensagem = "Impossível alterar a senha!";
                }
            }

            return View(vm);
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
                usuario.Nivel = "A"; // Tenhoue ajustar ainda
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

        private MudarNome ConstruirFormVMParaEdicao(int id)
        {
            Usuario registro = usuarioService.BuscarPorId(id);
            MudarNome vm = null;
            if (registro != null)
            {
                vm = ConverterFormVM(registro);
            }
            return vm;
        }

        private MeusDadosVM ConstruirFormVMParaEdicaoSenha(int id)
        {
            Usuario registro = usuarioService.BuscarPorId(id);
            MeusDadosVM vm = null;
            if (registro != null)
            {
                vm = ConverterFormVMSenha(registro);
            }
            return vm;
        }

        private MudarNome ConverterFormVM(Usuario reg)
        {
            MudarNome vm = new MudarNome();
            vm.IdUsuario = reg.IdUsuario;
            vm.Nome = reg.Nome;
            return vm;
        }

        private MeusDadosVM ConverterFormVMSenha(Usuario reg)
        {
            MeusDadosVM vm = new MeusDadosVM();
            vm.IdUsuario = reg.IdUsuario;
            return vm;
        }

    }
}
