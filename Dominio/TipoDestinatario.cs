using System;

namespace Dominio
{
    public class TipoDestinatario
    {
        public int Id_Tipo_Destinatario { get; set; }
        public string Descripcion { get; set; }

        public DateTime SistemaFecha { get; set; }

        public bool EnUso { get; set; }

        public string SistemaUsuario { get; set; }
        
    }
    
}