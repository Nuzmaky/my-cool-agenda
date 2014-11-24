using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public interface ICompromissoUsuarioDAO
    {
        void Adicionar(CompromissoUsuario entidade, DbTransaction transaction);

        List<CompromissoUsuario> Listar(int idUser);

        List<CompromissoUsuario> ListarPorGrupo(int idUser, int id);

        CompromissoUsuario BuscarPorId(int id, int idUser);

        List<CompromissoUsuario> ListarUsuariosDoCompromisso(int id, int idUser);

        List<CompromissoUsuario> VerificarUsuarioCriador(int id, int idUser);

        void Aceitar(int id, int idUser);

        void Rejeitar(int id, int idUser);

        

    }
}