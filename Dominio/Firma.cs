using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Firma
    {
        public int Id_Firma { get; set; }

        public List<Memorandum> Memos { get; set; } = new List<Memorandum>();

        public string Firmas { get; set; }

        public DateTime SistemaFecha { get; set; }

        public string SistemaUsuario { get; set; }
    }
}
