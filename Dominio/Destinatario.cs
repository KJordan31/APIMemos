using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Destinatario
    {

        public int Id_Destinatario {get; set; }

        public List<Memorandum> Memos {get; set; } = new List<Memorandum>();

        public string SistemaUsuario {get; set; }

        public DateTime SistemaFecha {get; set; }

        public string Usuario {get; set; }

        public Memorandum Memorandum { get; set; }

        
    }
}