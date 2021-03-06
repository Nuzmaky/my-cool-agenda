﻿using CoolAgenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
using System.Data.Common;

namespace CoolAgenda.Models
{
    public class GrupoUsuarioService : IGrupoUsuarioService
    {
        private IGrupoUsuarioDAO grupoUsuarioDAO;

        public GrupoUsuarioService()
        {
            grupoUsuarioDAO = new GrupoUsuarioDAO();
        }

        public List<GrupoUsuario> ListarGruposPessoa(int idUser)
        {
            var registros = grupoUsuarioDAO.ListarGruposPessoa(idUser);

            return registros;
        }

        public List<SelectListItem> ComboListarGruposUsuario(int idUser)
        {
            List<GrupoUsuario> registros = grupoUsuarioDAO.ListarGruposPessoa(idUser);
            List<SelectListItem> itens = ConverterRegistrosGrupoUserParaItens(registros);
            return itens;
        }

        public List<SelectListItem> ConverterRegistrosGrupoUserParaItens(List<GrupoUsuario> registros)
        {
            List<SelectListItem> itens = new List<SelectListItem>();
            itens.Add(new SelectListItem { Text = "<Selecione um Grupo>", Value = "" });
            foreach (var registro in registros)
            {
                itens.Add(new SelectListItem() { Text = registro.Grupo.Nome, Value = registro.IdGrupo.ToString() });
            }
            return itens;
        }

        public List<GrupoUsuario> ListarUsuarioPorGrupo(int idGrupo, string q, int idUser)
        {
            return grupoUsuarioDAO.ListarUsuarioPorGrupo(idGrupo, q, idUser);
        }

        public List<GrupoUsuario> Listar(int id)
        {
            return grupoUsuarioDAO.Listar(id);
        }

        public GrupoUsuario validaAdm(int id, int idUser)
        {
            return grupoUsuarioDAO.validaAdm(id, idUser);
        }

        public void AtivarPorId(int id)
        {
            grupoUsuarioDAO.AtivarPorId(id);
        }

        public void DesativarPorId(int id)
        {
            grupoUsuarioDAO.DesativarPorId(id);
        }

        public void DarPermissaoPorId(int id)
        {
            grupoUsuarioDAO.DarPermissaoPorId(id);
        }

        public void RetirarPermissaoPorId(int id)
        {
            grupoUsuarioDAO.RetirarPermissaoPorId(id);
        }

    }
}