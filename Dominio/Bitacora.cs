using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Bitacora
    {
        public int Id_Bitacora {get; set; }

        public string Observacion {get; set; }

        public DateTime SistemaFecha {get; set; }

        public string SistemaUsuario {get; set; }

        public List<Accion> Acciones {get; set; } =  new List<Accion>();
    }
}