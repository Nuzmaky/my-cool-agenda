using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CoolAgenda.Models
{
    public interface IUsuarioService
    {
        Usuario AutenticaUsuario(string email, string senha);

        Usuario BuscarPorEmail(string email);

        bool AtivarCadastro(string email);

        List<Validacao> ValidaUsuario(string email, string senha);

        List<Usuario> ListarUsuario();

        List<Validacao> ValidaEntidadeUsuario(Usuario user, bool edicao);

        List<Validacao> ValidaAdicionarUser(Usuario user);

        List<Validacao> ValidaAtualizarUser(Usuario user);
    }
}