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
    public interface IContatoService
    {
        List<Contato> Select();

        List<Contato> BuscarPorIdUsuario(int id);

        Contato BuscarPorId(int id);

        List<Validacao> ValidarEntidade(Contato entidade);

        List<Validacao> ValidaAdicionar(Contato entidade);

        List<Validacao> ValidaAtualizar(Contato entidade);

        List<Validacao> ValidaAdicionarUsuario(string email);

        void Insert(Contato entidade, List<Telefone> telefones);

        void InsertContato(Contato entidade);

        void Update(Contato entidade, List<Telefone> telefones);

        void DeleteById(int id);


        //Telefone
        void InsertTelefone(Contato contato, Telefone entidade, DbTransaction transacao);

        void UpdateTelefone(Contato contato, Telefone entidade, DbTransaction transacao);

        Telefone BuscarTelefonePorId(int id);
    }
}