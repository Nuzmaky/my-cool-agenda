using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoolAgenda.Filters;
using CoolAgenda.Models;
using System.Web.Mvc;
using CoolAgenda.Models.Services;
using CoolAgenda.ViewModels.NotaVM;
using System.Data.Common;
using CoolAgenda.Controllers.Utilidades;

namespace CoolAgenda.Controllers
{
    [FiltroAutenticacao("U")]
    public class NotaController : Controller
    {
            INotaService notaService;

            public NotaController()
            {
                notaService = new NotaService();
            }

            public ActionResult Index()
            {
                NotaVM vm = ConstruirIndexVM();
                return View(vm);
            }

            [ChildActionOnly]
            public ActionResult Form(int? id)
            {
                Usuario pUsuario = Session["Usuario"] as Usuario;
                int idUser = pUsuario.IdUsuario;

                NotaVM vm;

                if (id.HasValue)
                {
                    int idComp = id.Value;
                    Nota reg = notaService.BuscarNotaUsuarioCompromisso(idComp, idUser);

                    if (reg != null)
                    {
                        vm = ConstruirFormVMParaEdicao(reg.IdNota);
                        if (vm == null)
                            return new HttpNotFoundResult();
                    }
                    else
                    {
                        vm = ConstruirFormVMParaNovo(idComp);
                    }
                }
                else
                {
                    return new HttpNotFoundResult();
                }

                return PartialView(vm);
            }

            [HttpPost]
            public ActionResult Form(NotaVM vm)
            {
                if (ModelState.IsValid)
                {
                    Nota nota = ConverterFormVM(vm);

                    var erros = notaService.ValidarEntidade(nota);
                    if (erros.Count == 0)
                    {
                        if (vm.Edicao)
                            notaService.Update(nota);
                        else
                        {
                            Usuario usuario = Session["Usuario"] as Usuario;
                            nota.IdUsuario = usuario.IdUsuario;
                            notaService.Adicionar(nota);
                        }

                        return Json(new { redirectTo = Url.Action("Editar", "Compromisso" , new{ id = vm.IdCompromisso }) });
                    }
                    else
                    {
                        ModelState.AddModelErrors(erros);
                    }
                }

                return PartialView(vm);
            }

            //OUTROS METODOS

            private NotaVM ConstruirIndexVM()
            {
                NotaVM vm = new NotaVM();

                var registros = notaService.Listar();
                vm.ListaNota = registros;
                vm.TotalRegistros = registros.Count;
                return vm;
            }

            private NotaVM ConstruirFormVMParaEdicao(int id)
            {
                Nota registro = notaService.BuscarPorId(id);
                NotaVM vm = null;
                if (registro != null)
                {
                    vm = ConverterFormVM(registro);
                    vm.Edicao = true;
                }
                return vm;
            }

            private NotaVM ConverterFormVM(Nota reg)
            {
                NotaVM vm = new NotaVM();
                vm.IdNota = reg.IdNota;
                vm.IdUsuario = reg.IdUsuario;
                vm.IdCompromisso = reg.IdCompromisso;
                vm.Texto = reg.Texto;
                vm.Ativo = reg.Ativo;
                
                return vm;
            }

            private NotaVM ConstruirFormVMParaNovo(int idComp)
            {
                NotaVM vm = new NotaVM();
                vm.Edicao = false;
                vm.IdCompromisso = idComp;
                return vm;
            }

            private Nota ConverterFormVM(NotaVM vm)
            {
                Nota reg = new Nota();
                reg.IdNota = vm.IdNota;
                reg.IdUsuario = vm.IdUsuario;
                reg.IdCompromisso = vm.IdCompromisso;
                reg.Texto = vm.Texto;
                reg.Ativo = vm.Ativo;
                
                return reg;
            }
        }

    }

