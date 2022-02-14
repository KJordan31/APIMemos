using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Memorandum
    {
        public int Id { get; set; }

        public string Codigo { get; set; }

        public string SistemaUsuario { get; set; }

        public List<TipoMemorandum> Tipos { get; set; } = new List<TipoMemorandum>();

        public string Asunto { get; set; }

        public List<Estado> Estados { get; set; } = new List<Estado>();

        public int Id_Area { get; set; }

        public DateTime SistemaFecha { get; set; }

        public DateTime Fecha_Modificacion { get; set; }

        public List<Destinatario> Destinatarios { get; set;} = new List<Destinatario>();
    }
}
