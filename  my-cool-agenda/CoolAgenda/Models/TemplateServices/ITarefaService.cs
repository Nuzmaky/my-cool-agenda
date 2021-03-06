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
    public interface ITarefaService
    {
        List<Tarefa> Listar(int id);

        List<Tarefa> ListarReq(int id);

        List<Validacao> ValidarEntidade(Tarefa entidade);

        List<Validacao> ValidaAdicionar(Tarefa entidade);

        void Adicionar(Tarefa entidade);

        void Atualizar(Tarefa entidade);

        List<Validacao> ValidaAtualizar(Tarefa entidade);

        Tarefa BuscarPorId(int id);

        //List<Tarefa> ListarPorGrupo(int idUser, int id);

        List<Tarefa> ListarId(int idUser);

        void DesativarPorId(int id);

        void ConcluirPorId(int id);
   }
}