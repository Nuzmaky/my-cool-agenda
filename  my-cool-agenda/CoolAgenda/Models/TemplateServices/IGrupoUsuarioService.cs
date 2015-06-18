using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoolAgenda.Models
{
    public interface IGrupoUsuarioService
    {
        List<GrupoUsuario> ListarGruposPessoa(int idUser);

        List<SelectListItem> ComboListarGruposUsuario(int idUser);

        List<SelectListItem> ConverterRegistrosGrupoUserParaItens(List<GrupoUsuario> registros);

        List<GrupoUsuario> ListarUsuarioPorGrupo(int idGrupo, string q, int idUser);

        List<GrupoUsuario> Listar(int i);

        GrupoUsuario validaAdm(int id, int idUser);

        void AtivarPorId(int id);

        void DesativarPorId(int id);

        void DarPermissaoPorId(int id);

        void RetirarPermissaoPorId(int id);

    }
}