﻿using System;
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

        void AtivaUsuarioGrupo(Usuario usuario);

        List<Usuario> Listar();

        Usuario BuscarPorId(int id);

        Usuario BuscarPorId(int id, DbTransaction transaction);

        Usuario BuscarPorEmail(string email);

        void AtualizarNome(Usuario entidade);

        void AtualizarSenha(Usuario entidade);
    }
}