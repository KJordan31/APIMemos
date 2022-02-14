using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Adjunto
    {
        public int Id_Adjuntos { get; set; }

        public string Adjuntos { get; set; }

        public DateTime SistemaFecha { get; set; }

        public string SistemaUsuario { get; set; }

        public List<Memorandum> Memos { get; set;} = new List<Memorandum>();
    }
}
