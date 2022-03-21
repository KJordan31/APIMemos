using System;

namespace Dominio
{
    public class Memorandum
    {
        public int Id { get; set; }

        public string Codigo { get; set; }

        public string SistemaUsuario { get; set; }

        public int? Id_Tipo { get; set; }

        public virtual TipoMemorandum TipoMemorandum { get; set; }

        public string Asunto { get; set; }

        public string DestinatarioUsu { get; set; }

        public int? Id_Tipo_Destinatario { get; set; }

        public int? Id_Estado { get; set; }

        public virtual Estado Estado { get; set; }

        public int Id_Area { get; set; }

        public DateTime SistemaFecha { get; set; }

        public DateTime Fecha_Modificacion { get; set; }

        public virtual Destinatario Destinatario { get; set; }

        public virtual ContenidoMemo Contenido { get; set; }

        public virtual int Count { get; }

        public int Id_Bitacora { get; set; }
    }
}
