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
        Nota BuscarPorId(int id);
        
        //Update
        void Update(Nota nota);
       
        //Conversão
        Nota ConverterParaTipoClasse(OleDbDataReader dr);
        
    }
}