using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Common;

namespace CoolAgenda.Models
{
    public interface IGrupoService
    {
        List<Grupo> Listar();
         
        List<Validacao> ValidarEntidade(Grupo entidade);

        List<Validacao> ValidaAdicionar(Grupo entidade);

        void Adicionar(Grupo entidade, Usuario user);

        void Atualizar(Grupo entidade);

        List<Validacao> ValidaAtualizar(Grupo entidade);

        Grupo BuscarPorId(int id);

        int ProximoIdGrupo(DbTransaction transaction);

        void AddGrupo(Grupo grupo, DbTransaction transaction);

        // Usuario

        int ProximoIdUser(DbTransaction transaction);

        void AddUsuario(Usuario user, DbTransaction transaction);


    }
}