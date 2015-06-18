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
        List<Tarefa> Listar(int id);
        Tarefa BuscarPorId(int id);
        List<Tarefa> ListarReq(int id);

        //Update
        //void Update(Tarefa tarefa, DbTransaction transacao);
        void Update(Tarefa tarefa);
        
        //Conversão
        Tarefa ConverterParaTipoClasse(OleDbDataReader dr);        
        List<Tarefa> populaTarefas(Tarefa tarefa);

        Tarefa ConverteCompTare(OleDbDataReader dr);

        //List<Tarefa> ListarPorGrupo(int idUser, int id);

        List<Tarefa> ListarId(int idUser);

        void DesativarPorId(int id);

        void ConcluirPorId(int id);

    }
}

    
