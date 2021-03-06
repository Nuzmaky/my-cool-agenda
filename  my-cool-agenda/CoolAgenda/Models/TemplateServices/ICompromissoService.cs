﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Common;

namespace CoolAgenda.Models
{
    public interface ICompromissoService
    {
        List<Validacao> ValidarEntidade(Compromisso entidade);

        void Adicionar(Compromisso entidade, List<CompromissoUsuario> cUser, List<CompromissoContato> cContato);

        void Atualizar(Compromisso entidade);

        List<Validacao> ValidaAdicionar(Compromisso entidade);

        List<Validacao> ValidaAtualizar(Compromisso entidade);

        List<SelectListItem> ListarCores();

        int ProximoIdCompromisso(DbTransaction transaction);

        void AddCompromisso(Compromisso entidade, DbTransaction transaction);

        void AddCompromissoUsuario(CompromissoUsuario entidade, DbTransaction transaction);

        void AddCompromissoContato(CompromissoContato entidade, DbTransaction transaction);

        void Excluir(int id);
    }
}