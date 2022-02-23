using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class Adjunto
    {
        public int Id_Adjuntos { get; set; }

        public int? Id { get; set; }

        public string Adjuntos { get; set; }

        public DateTime SistemaFecha { get; set; }

        public string SistemaUsuario { get; set; } 
       
        public virtual Memorandum IdMemos {get; set;}
    }
}
  