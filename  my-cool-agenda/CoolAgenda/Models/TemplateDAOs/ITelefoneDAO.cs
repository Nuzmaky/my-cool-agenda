﻿using CoolAgenda.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public interface ITelefoneDAO
    {
        void Insert(Contato contato, Telefone telefone, DbTransaction transacao);
        void Update(Contato contato, Telefone telefone, DbTransaction transacao);
        

        List<Telefone> Select();
        List<Telefone> ListarPorIdContato(int id);
        List<Telefone> ListarPorIdUsuario(int id);
        Telefone SelectById(int id);
    }
}