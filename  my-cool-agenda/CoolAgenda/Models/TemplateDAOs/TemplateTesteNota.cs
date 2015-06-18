using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CoolAgenda.Models.TemplateDAOs
{
    abstract public class TemplateNota:INotaDAO
    {
        //Insert
        abstract public void Adcionar(Nota nota);

        //Select
        abstract public List<Nota> Listar();

        abstract public List<Nota> Listar(int id);

        abstract public Nota BuscarPorId(int id);
        
        //Update
        abstract public void Update(Nota nota);
       
        //Conversão
        abstract public Nota ConverterParaTipoClasse(OleDbDataReader dr);

        abstract public Nota BuscarNotaUsuarioCompromisso(int id, int idUser);

        abstract public List<Nota> VerificarUsuarioCriador(int id, int idUser);
        
    }
}

    