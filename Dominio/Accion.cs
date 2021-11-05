using System;

namespace Dominio
{
    public class Accion
    {
        public int Id_Accion { get; set; }
        public string Descripcion { get; set; }
        public DateTime SistemaFecha { get; set; }
        public string SistemaUsuario { get; set; }
    }
}