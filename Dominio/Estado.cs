using System;

namespace Dominio
{
    public class Estado
    {
        public int Id_Estado { get; set; }

        public string Descripcion { get; set; }

        public DateTime SistemaFecha { get; set; }

        public bool EnUso { get; set; }

        public string SistemaUsuario { get; set; }

        public int Id_Bitacora { get; set; }
    }
}
