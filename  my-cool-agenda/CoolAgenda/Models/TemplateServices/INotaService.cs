using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CoolAgenda.Models.Services;
using System.Data.Common;

namespace CoolAgenda.Models
{
    public interface INotaService
    {
        List<Nota> Listar();
  
        void Adicionar(Nota entidade);
        
        void Update(Nota entidade);

        List<Validacao> ValidaAtualizar(Nota entidade, int id);
       
        List<Validacao> ValidarEntidade(Nota entidade, int id);
        
        List<Validacao> ValidaAdicionar(Nota entidade);

        Nota BuscarPorId(int id);

        Nota BuscarNotaUsuarioCompromisso(int id, int idUser);

        List<Nota> Listar(int id);

    }
}