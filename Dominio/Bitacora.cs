using System;

namespace Dominio
{
    public class Bitacora
    {
        public int Id_Bitacora {get; set; }

        public string Observacion {get; set; }

        public DateTime SistemaFecha {get; set; }

        public string SistemaUsuario {get; set; }

        public int Id_Accion {get; set; }
    }
}