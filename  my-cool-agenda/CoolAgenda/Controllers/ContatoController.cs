﻿using CoolAgenda.Filters;
using CoolAgenda.Models;
using CoolAgenda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolAgenda.Controllers.Utilidades;
using CoolAgenda.ViewModels.GrupoVM;
using CoolAgenda.ViewModels.AgendaVM;
using CoolAgenda.Models.Entidades;

namespace CoolAgenda.Controllers
{
    public class ContatoController : Controller
    {
        //
        // GET: /Contato/        

        ContatoTel contatotel = new ContatoTel();
        UsuarioDAO usuarioDao = new UsuarioDAO();

        private IGrupoUsuarioService grupoUsuarioService;
        private IUsuarioService usuarioService;
        private IContatoService contatoService;
        private ITelefoneService telefoneService;

        public ContatoController()
        {
            grupoUsuarioService = new GrupoUsuarioService();
            usuarioService = new UsuarioService();
            contatotel = new ContatoTel();
            contatoService = new ContatoService();
            telefoneService = new TelefoneService();
        }


        //Index principal
        [FiltroAutenticacao]
        public ActionResult Index()
        {
            //Pega o usuário na sessão
            Usuario usuario = Session["Usuario"] as Usuario;
            int idUsuario = usuario.IdUsuario;

            ContatoVM vm = ConstruirContatoVM(idUsuario);
            return View(vm);
        }

        // Cadastro de Contatos
        public ActionResult Form(int? id)
        {
            ContatoVM vm;
            if (id.HasValue)
            {
                vm = ConstruirFormVMParaEdicao(id.Value);

                // Telfones
                List<Telefone> telefonesEntidades = contatoService.BuscarTelefonePorId(vm.IdContato);

                if (telefonesEntidades != null || telefonesEntidades.Count != 0)
                {
                    vm.Telefones = new List<string>();
                    foreach (var telefoneEntidade in telefonesEntidades)
                    {
                        string telefoneFormatado = FormatarNumeroTelefone(telefoneEntidade.NumeroTelefone);
                        vm.Telefones.Add(telefoneFormatado);
                    }
                }

                if (vm == null)
                    return new HttpNotFoundResult();
            }
            else
            {
                vm = ConstruirFormVMParaNovo();
            }
            return View(vm);
        }

        //Convite de Contatos
        public ActionResult Convida(int? id, UsuarioVM vmUser)
        {
            //Pega o usuário na sessão
            Usuario usuario = Session["Usuario"] as Usuario;
            int idUsuario = usuario.IdUsuario;
            vmUser.IdUsuario = idUsuario;

            ContatoVM vmContato = new ContatoVM();

            //Se tiver um valor, vai para Edição
            if (id.HasValue)
            {
                vmContato = ConstruirFormVMParaEdicao(id.Value);
                if (vmContato == null)
                    return new HttpNotFoundResult();
                else
                {
                    //Verifica se já existe
                    var erros = contatoService.ValidaAdicionarUsuario(vmContato.Email);
                    if (erros.Count == 0)
                    {
                        // -- Aqui deve-se criar um novo usuario, com os dados do contato!
                        Usuario registro = ConverterUsuarioFormVM(vmContato);

                        //Envia E-mail para o contato
                        try
                        {
                            ContatoService.EnviaEmailCadastro(registro.Email, registro.Nome, registro.Senha);

                            //Adiciona no banco com senha padrão
                            usuarioDao.Adicionar(registro);


                            // Pega os grupos em que o usuário está cadastrado
                            var usuarioGrupos = grupoUsuarioService.ListarGruposPessoa(idUsuario);
                            List<GrupoUsuario> listaGrupoUsuario = new List<GrupoUsuario>();
                            listaGrupoUsuario = usuarioGrupos;

                            //Joga o Id do Novo Usuario para cadastrar no grupo
                            //SELECT
                            Usuario novoUsuarioGrupo = usuarioDao.BuscarPorEmail(registro.Email);

                            // Joga o Id do novo Usuario no GrupoUsuario
                            for (int i = 0; i < listaGrupoUsuario.Count; i++)
                            {
                                listaGrupoUsuario[i].IdUsuario = novoUsuarioGrupo.IdUsuario;
                                listaGrupoUsuario[i].Ativo = "N";
                            }

                            // 

                            // Adiciona  o Usuário no Grupo
                            contatoService.InsertContatoGrupo(listaGrupoUsuario);

                            ViewBag.Mensagem = "Usuário convidado com sucesso!";
                        }
                        catch
                        {
                            ViewBag.Mensagem = "Impossível enviar o e-mail. Verifique os dados do contato.";
                        }


                        return View();
                    }
                    else
                    {
                        erros.Add(new Validacao("O Usuário já está cadastrado no Sistema!"));
                        ModelState.AddModelErrors(erros);
                        ViewBag.Mensagem = "Usuário já existe no sistema.";
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Form(ContatoVM vm)
        {
            //Pega o usuário na sessão
            Usuario usuario = Session["Usuario"] as Usuario;
            int idUsuario = usuario.IdUsuario;
            vm.IdUsuario = idUsuario;


            if (ModelState.IsValid)
            {
                // Recebe o Contato e o Telefone
                Contato contato = ConverterFormVM(vm);
                List<Telefone> telefones = ConverterCadVMParaTelefones(vm);

                var erros = contatoService.ValidarEntidade(contato);
                if (erros.Count == 0)
                {
                    if (vm.Edicao)
                    {
                        
                        vm = ConstruirContatoVM(idUsuario);

                        List<Telefone> tel = telefoneService.ListarPorIdContato(contato.IdContato);

                        if (tel.Count == 1 && telefones.Count==2)
                        {
                            tel.Add(telefones[1]);
                            tel[1].NumeroTelefone = "";
                        }

                        if(telefones.Count==1 && tel.Count==2)
                        {
                            telefones.Add(tel[1]);
                            telefones[1].NumeroTelefone = "";
                        }

                        for (int i = 0; i < telefones.Count; i++)
                            {
                                telefones[i].IdTelefone = tel[i].IdTelefone;
                                telefones[i].IdContato = tel[i].IdContato;
                            }
                        

                        // Atualiza Telefone e Contato
                        contatotel.UpdateContatoTel(contato, telefones);
                    }
                    else
                    {
                        vm = ConstruirContatoVM(idUsuario);
                        int i = vm.ListaContato.Count - 1;
                        contato.IdContato = vm.ListaContato[i].IdContato;

                        //Insere Telefone e Contato
                        contatoService.InsertContato(contato);
                        contatoService.Insert(contato, telefones);

                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelErrors(erros);
                }
            }
            return View(vm);
        }

        private ContatoVM ConstruirContatoVM(int id)
        {
            ContatoVM vm = new ContatoVM();

            var registros = contatoService.BuscarPorIdUsuario(id);
            vm.ListaContato = registros;
            vm.ListaTelefone = telefoneService.ListarPorIdUsuario(id);
            vm.TotalRegistros = registros.Count;
            return vm;
        }

        private ContatoVM ConstruirFormVMParaEdicao(int id)
        {
            Contato registro = contatoService.BuscarPorId(id);
            ContatoVM vm = null;
            if (registro != null)
            {
                vm = ConverterFormVM(registro);
                vm.Edicao = true;
            }
            return vm;
        }

        private ContatoVM ConverterFormVM(Contato reg)
        {
            ContatoVM vm = new ContatoVM();
            vm.IdContato = reg.IdContato;
            vm.IdUsuario = reg.IdUsuario;
            vm.Nome = reg.Nome;
            vm.Apelido = reg.Apelido;
            vm.Email = reg.Email;
            vm.ListaTelefone = telefoneService.ListarPorIdContato(vm.IdContato);


            if (vm.ListaTelefone[0].NumeroTelefone != null)
                vm.telefoneUm = vm.ListaTelefone[0].NumeroTelefone;

            if (vm.ListaTelefone.Count > 1)
            {
                if (vm.ListaTelefone[1].NumeroTelefone != null)
                    vm.telefoneDois = vm.ListaTelefone[1].NumeroTelefone;
            }


            vm.Endereco = reg.Endereco;
            vm.DataNascimento = reg.DataNascimento;

            return vm;
        }

        private Grupo ConverterGrupoVM(GrupoFormVM vm)
        {
            Grupo reg = new Grupo();
            reg.IdGrupo = vm.IdGrupo;
            reg.Nome = vm.Nome;
            reg.FlagAtivo = (vm.FlagAtivo ? "S" : "N");

            return reg;
        }

        private Contato ConverterFormVM(ContatoVM vm)
        {
            Contato reg = new Contato();
            reg.IdUsuario = vm.IdUsuario;
            reg.IdContato = vm.IdContato;
            reg.Nome = vm.Nome;
            reg.Apelido = vm.Apelido;
            reg.Email = vm.Email;
            reg.Endereco = vm.Endereco;
            reg.DataNascimento = vm.DataNascimento;

            return reg;
        }

        private Usuario ConverterUsuarioFormVM(ContatoVM vm)
        {
            Usuario reg = new Usuario();
            // Joga dados do Contato para Usuario
            reg.IdUsuario = vm.IdUsuario;
            reg.Nome = vm.Nome;
            reg.Email = vm.Email;

            //Padrão
            reg.Senha = "123@abc";
            reg.Nivel = "U";
            reg.Ativo = "N";

            return reg;
        }

        private ContatoVM ConstruirFormVMParaNovo()
        {
            ContatoVM vm = new ContatoVM();
            vm.Edicao = false;
            return vm;
        }


        // --- Telefone

        private List<Telefone> ConverterCadVMParaTelefones(ContatoVM vm)
        {
            List<Telefone> telefones = new List<Telefone>();

            string numeroTelefoneUm = vm.telefoneUm;
            if (!String.IsNullOrEmpty(numeroTelefoneUm))
            {
                Telefone telefoneUm = new Telefone();
                telefoneUm.NumeroTelefone = RemoverFormatacaoTelefone(numeroTelefoneUm);
                telefones.Add(telefoneUm);
            }

            string numeroTelefoneDois = vm.telefoneDois;
            if (!String.IsNullOrEmpty(numeroTelefoneDois))
            {
                Telefone telefoneDois = new Telefone();
                telefoneDois.NumeroTelefone = RemoverFormatacaoTelefone(numeroTelefoneDois);
                telefones.Add(telefoneDois);
            }

            return telefones;
        }

        private string RemoverFormatacaoTelefone(string telefoneFormatado)
        {
            string telefoneApenasNumeros = telefoneFormatado.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
            return telefoneApenasNumeros;
        }

        private string FormatarNumeroTelefone(string telefoneApenasNumero)
        {
            if (telefoneApenasNumero != "")
            {
                string telefoneFormatado = telefoneApenasNumero.Insert(0, "(").Insert(3, ")").Insert(4, " ");
                return telefoneFormatado;
            }

            else { return telefoneApenasNumero; }
        }


        //Desativar
        public JsonResult Desativar(int id)
        {
            contatoService.InativarContato(id);
            return Json(new JsonActionResultModel("Registro desativado com sucesso."));
        }
    }
}