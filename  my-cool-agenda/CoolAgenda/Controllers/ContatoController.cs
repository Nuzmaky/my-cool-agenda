﻿using CoolAgenda.Filters;
using CoolAgenda.Models;
using CoolAgenda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolAgenda.Controllers.Utilidades;

namespace CoolAgenda.Controllers
{
    public class ContatoController : Controller
    {
        //
        // GET: /Contato/

        Contato contato = new Contato();
        Usuario usuario = new Usuario();
        ContatoDAO contatoDAO = new ContatoDAO();
        UsuarioDAO usuarioDAO = new UsuarioDAO();

        IUsuarioService usuarioService;
        IContatoService contatoService;
        ITelefoneService telefoneService;

        public ContatoController()
        {
            usuarioService = new UsuarioService();
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
        public ActionResult Convida(int? id)
        {
            ContatoVM vm;
            if (id.HasValue)
            {
                vm = ConstruirFormVMParaEdicao(id.Value);
                if (vm == null)
                    return new HttpNotFoundResult();
                else
                {
                    // -- Aqui deve-se criar um novo usuario, com os dados do contato!
                    Usuario registro = ConverterUsuarioFormVM(vm);

                    //Verifica se já existe
                    var erros = contatoService.ValidaAdicionarUsuario(registro.Email);
                    if (erros.Count == 0)
                    {
                        //Adiciona no banco com senha padrão
                        usuarioDAO.Adicionar(registro);

                        //Envia E-mail para o contato
                        ContatoService.EnviaEmailCadastro(vm.Email, vm.Nome, registro.Senha);
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
                Contato contato = ConverterFormVM(vm);
                List<Telefone> telefones = ConverterCadVMParaTelefones(vm);

                var erros = contatoService.ValidarEntidade(contato);
                if (erros.Count == 0)
                {
                    if (vm.Edicao)
                    {
                        vm = ConstruirContatoVM(idUsuario);
                        contatoService.Update(contato, telefones);
                    }
                    else
                    {
                        // Insere Contato
                        contatoService.InsertContato(contato);

                        vm = ConstruirContatoVM(idUsuario);
                        int i = vm.ListaContato.Count - 1;
                        contato.IdContato = vm.ListaContato[i].IdContato;
                        //Insere Telefone do contato
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
            vm.Email = reg.Email;
            vm.Endereco = reg.Endereco;

            return vm;
        }

        private Contato ConverterFormVM(ContatoVM vm)
        {
            Contato reg = new Contato();
            reg.IdUsuario = vm.IdUsuario;
            reg.IdContato = vm.IdContato;
            reg.Nome = vm.Nome;
            reg.Email = vm.Email;
            reg.Endereco = vm.Endereco;

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
                telefoneUm.NumeroTelefone= RemoverFormatacaoTelefone(numeroTelefoneUm);
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
    }
}