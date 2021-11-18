using System;

namespace Dominio
{
    public class TipoMemorandum{
        public int Id_Tipo { get; set; }

        public string Tipo { get; set; }

        public DateTime SistemaFecha { get; set; }

        public bool EnUso { get; set; }

        public string SistemaUsuario {get; set; }
    }
    
}