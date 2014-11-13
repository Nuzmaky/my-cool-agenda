using CoolAgenda.Filters;
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
        ContatoDAO contatoDAO = new ContatoDAO();

        IContatoService contatoService;

        public ContatoController()
        {
            contatoService = new ContatoService();
        }

        [FiltroAutenticacao]
        public ActionResult Index()
        {
            ContatoVM vm = ConstruirContatoVM();
            return View(vm);
        }

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
            return View (vm);
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
                Contato registro = ConverterFormVM(vm);
                List<Telefone> telefones = ConverterCadVMParaTelefone(vm);

                var erros = contatoService.ValidarEntidade(registro);
                if (erros.Count == 0)
                {
                    if (vm.Edicao)
                        contatoService.Update(registro);
                    else
                        contatoService.Insert(registro, telefones);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelErrors(erros);
                }
            }
            return View(vm);
        }

        private ContatoVM ConstruirContatoVM()
        {
            ContatoVM vm = new ContatoVM();

            var registros = contatoService.Select();
            vm.ListaContato = registros;
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
            reg.IdContato = vm.IdContato;
            reg.IdUsuario = vm.IdUsuario;
            reg.Nome = vm.Nome;
            reg.Email = vm.Email;
            reg.Endereco = vm.Endereco;

            return reg;
        }

        private ContatoVM ConstruirFormVMParaNovo()
        {
            ContatoVM vm = new ContatoVM();
            vm.Edicao = false;
            return vm;
        }


        // --- Telefone

        private List<Telefone> ConverterCadVMParaTelefone(ContatoVM vm)
        {
            List<Telefone> telefones = new List<Telefone>();

            string numTelefone = vm.Telefone;
            if (!String.IsNullOrEmpty(numTelefone))
            {
                Telefone telefone = new Telefone();
                telefone.NumeroTelefone = RemoverFormatacaoTelefone(numTelefone);
                telefones.Add(telefone);
            }
            return telefones;
        }

        private string RemoverFormatacaoTelefone(string telefoneFormatado)
        {
            string telefoneApenasNumeros = telefoneFormatado.Replace("(", "").Replace(")", "").Replace(" ", "");
            return telefoneApenasNumeros;
        }
    }
}
