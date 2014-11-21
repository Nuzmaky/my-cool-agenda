using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoolAgenda.Models
{
    public interface ICompromissoUsuarioService
    {
        List<CompromissoUsuario> Listar(int idUser);

        List<CompromissoUsuario> ListarPorGrupo(int idUser, int id);

        CompromissoUsuario BuscarPorId(int id, int idUser);

        List<CompromissoUsuario> ListarUsuariosDoCompromisso(int id, int idUser);

        void Aceitar(int id, int userId);

        void Rejeitar(int id, int userId);

        List<Validacao> ValidaAtualizar(CompromissoUsuario entidade);
    }
}