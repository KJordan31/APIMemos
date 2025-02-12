using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Firma
    {
        public int Id_Firma { get; set; }

        public int? Id {get; set;}

        public virtual Memorandum IdMemos { get; set; } 

        public string Firmas { get; set; }

        public DateTime SistemaFecha { get; set; }

        public string SistemaUsuario { get; set; }
    }
}
