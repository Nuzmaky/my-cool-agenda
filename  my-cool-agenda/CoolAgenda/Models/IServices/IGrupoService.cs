using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoolAgenda.Models
{
    public interface IGrupoService
    {
        List<Grupo> Listar();
         
        List<Validacao> ValidarEntidade(Grupo entidade);

        List<Validacao> ValidaAdicionar(Grupo entidade);

        void Adicionar(Grupo entidade);

        void Atualizar(Grupo entidade);

        List<Validacao> ValidaAtualizar(Grupo entidade);

        Grupo BuscarPorId(int id);
    }
}