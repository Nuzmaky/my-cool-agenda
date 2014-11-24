using CoolAgenda.Filters;
using CoolAgenda.Models;
using CoolAgenda.ViewModels.GrupoUsuarioVM;
using CoolAgenda.Controllers.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace CoolAgenda.Controllers
{
    [FiltroAutenticacao("U")]
    public class GrupoUsuarioController : Controller
    {
        IGrupoUsuarioService grupoUsuarioService;

        public GrupoUsuarioController()
        {
            grupoUsuarioService = new GrupoUsuarioService();
        }

        public ActionResult Index(int id)
        {
            GrupoUsuarioIndexVM vm;

            Usuario pUsuario = Session["Usuario"] as Usuario;
            int idUser = pUsuario.IdUsuario;

            if (id > 1)
            {
                var verificaADM = grupoUsuarioService.validaAdm(id, idUser);
                if (verificaADM != null)
                {
                    vm = ConstruirPagGrupoUsuario(id);
                    if (vm == null)
                        return new HttpNotFoundResult();
                }
                else
                {
                    return new HttpNotFoundResult();
                }
            }
            else
            {
                return new HttpNotFoundResult();
            }

            return View(vm);
        }

        public JsonResult Desativar(int id)
        {
            grupoUsuarioService.DesativarPorId(id);
            return Json(new JsonActionResultModel("Registro desativado!"));
        }

        public JsonResult Ativar(int id)
        {
            grupoUsuarioService.AtivarPorId(id);
            return Json(new JsonActionResultModel("Registro ativado!"));
        }

        public JsonResult DarPermissao(int id)
        {
            grupoUsuarioService.DarPermissaoPorId(id);
            return Json(new JsonActionResultModel("Permissões concedidas!"));
        }

        public JsonResult RetirarPermissao(int id)
        {
            grupoUsuarioService.RetirarPermissaoPorId(id);
            return Json(new JsonActionResultModel("Permissões retiradas!"));
        }

        private GrupoUsuarioIndexVM ConstruirPagGrupoUsuario(int id)
        {
            GrupoUsuarioIndexVM vm = new GrupoUsuarioIndexVM();

            var registros = grupoUsuarioService.Listar(id);

            vm.Lista = registros;
            vm.TotalRegistros = registros.Count();

            return vm;
        }
    }
}
