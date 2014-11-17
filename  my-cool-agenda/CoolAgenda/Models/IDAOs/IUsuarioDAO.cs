using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public interface IUsuarioDAO
    {
        void Adicionar(Usuario usuario, DbTransaction transaction);

        int ProximoIdUser(DbTransaction transaction);

        void AtivaCadastro(Usuario usuario);

        List<Usuario> Listar();

        Usuario BuscarPorId(int id);
    }
}