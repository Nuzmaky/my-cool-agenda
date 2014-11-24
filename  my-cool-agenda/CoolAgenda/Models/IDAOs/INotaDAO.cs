using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models
{
    public interface INotaDAO
    {
        //Insert
        void Adcionar(Nota nota);

        //Select
        List<Nota> Listar();

        List<Nota> Listar(int id);

        Nota BuscarPorId(int id);
        
        //Update
        void Update(Nota nota);
       
        //Conversão
        Nota ConverterParaTipoClasse(OleDbDataReader dr);

        Nota BuscarNotaUsuarioCompromisso(int id, int idUser);

        List<Nota> VerificarUsuarioCriador(int id, int idUser);
        
    }
}