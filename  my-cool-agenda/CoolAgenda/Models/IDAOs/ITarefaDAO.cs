using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public interface ITarefaDAO
    {
        //Insert
        void Adcionar(Tarefa tarefa);

        //Select
        List<Tarefa> Listar();
        Tarefa BuscarPorId(int id);

        //Update
        void Update(Tarefa tarefa, DbTransaction transacao);
        
        //Conversão
        Tarefa ConverterParaTipoClasse(OleDbDataReader dr);        
        List<Tarefa> populaTarefas(Tarefa tarefa);

        Tarefa ConverteCompTare(OleDbDataReader dr);

        
        


    }
}

    
