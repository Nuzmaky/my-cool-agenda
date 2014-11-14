using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CoolAgenda.Models
{
    public interface IContatoService
    {
        List<Contato> Select();

        List<Validacao> ValidarEntidade(Contato entidade);

        List<Validacao> ValidaAdicionar(Contato entidade);

        List<Validacao> ValidaAtualizar(Contato entidade);

        List<Validacao> ValidaAdicionarUsuario(string email);

        void Insert(Contato entidade, List<Telefone> telefones);

        void Update(Contato entidade);

        void DeleteById(int id);

        Contato BuscarPorId(int id);
    }
}