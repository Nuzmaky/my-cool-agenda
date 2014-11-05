using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoolAgenda.Models
{
    public interface ICompromissoService
    {
        List<Validacao> ValidarEntidade(Compromisso entidade);

        void Adicionar(Compromisso entidade);

        List<Validacao> ValidaAdicionar(Compromisso entidade);

        void Atualizar(Compromisso entidade);

        List<Validacao> ValidaAtualizar(Compromisso entidade);
    }
}