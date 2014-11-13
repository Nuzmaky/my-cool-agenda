using CoolAgenda.Models;
using CoolAgenda.Filters;
using CoolAgenda.ViewModels.GrupoVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoolAgenda.Controllers.Utilidades;

namespace CoolAgenda.Controllers
{

    [FiltroAutenticacao("A")]
    public class GrupoController : Controller
    {
        //
        // GET: /Grupo/
        Grupo grupo = new Grupo();
        GrupoDAO grupoDAO = new GrupoDAO();

        IGrupoService grupoService;

        public GrupoController()
        {
            grupoService = new GrupoService();
        }

        [FiltroAutenticacao]
        public ActionResult Index()
        {
            GrupoIndexVM vm = ConstruirIndexVM();
            return View(vm);
        }

        public ActionResult Form(int? id)
        {
            GrupoFormVM vm;
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

        [HttpPost]
        public ActionResult Form(GrupoFormVM vm)
        {
            if (ModelState.IsValid)
            {
                Grupo registro = ConverterFormVM(vm);

                var erros = grupoService.ValidarEntidade(registro);
                if (erros.Count == 0)
                {
                    if (vm.Edicao)
                        grupoService.Atualizar(registro);
                    else
                        grupoService.Adicionar(registro);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelErrors(erros);
                }
            }

            return View(vm);
        }

        //OUTROS METODOS

        private GrupoIndexVM ConstruirIndexVM()
        {
            GrupoIndexVM vm = new GrupoIndexVM();

            var registros = grupoService.Listar();
            vm.ListaGrupo = registros;
            vm.TotalRegistros = registros.Count;
            return vm;
        }

        private GrupoFormVM ConstruirFormVMParaEdicao(int id)
        {
            Grupo registro = grupoService.BuscarPorId(id);
            GrupoFormVM vm = null;
            if (registro != null)
            {
                vm = ConverterFormVM(registro);
                vm.Edicao = true;
            }
            return vm;
        }

        private GrupoFormVM ConverterFormVM(Grupo reg)
        {
            GrupoFormVM vm = new GrupoFormVM();
            vm.IdGrupo = reg.IdGrupo;
            vm.Nome = reg.Nome;
            vm.CNPJ = reg.CNPJ;
            vm.FlagAtivo = reg.FlagAtivo.Equals("S");

            return vm;
        }

        private GrupoFormVM ConstruirFormVMParaNovo()
        {
            GrupoFormVM vm = new GrupoFormVM();
            vm.Edicao = false;
            return vm;
        }

        private Grupo ConverterFormVM(GrupoFormVM vm)
        {
            Grupo reg = new Grupo();
            reg.IdGrupo = vm.IdGrupo;
            reg.Nome = vm.Nome;
            reg.CNPJ = RemoverFormatacaoCNPJ(vm.CNPJ);
            reg.FlagAtivo = (vm.FlagAtivo ? "S" : "N");
            
            return reg;
        }

        private string RemoverFormatacaoCNPJ(string cpnjFormatado)
        {
            string cnpjNumeros = cpnjFormatado.Replace(".", "").Replace("-", "").Replace("/", "");
            return cnpjNumeros;
        }
    }
}
