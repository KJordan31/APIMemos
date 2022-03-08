using System;

namespace Dominio
{
    public class Adjunto
    {
        public int Id_Adjuntos { get; set; }       

        public string Adjuntos { get; set; }

        public DateTime SistemaFecha { get; set; }

        public string SistemaUsuario { get; set; } 

        public int? Id { get; set; }
       
        public virtual Memorandum Memorandum {get; set;}
    }
}
  