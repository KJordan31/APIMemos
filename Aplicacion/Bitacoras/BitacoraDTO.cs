using Aplicacion.Estados;
using Aplicacion.Memos;

namespace Aplicacion.Bitacoras
{
    public class BitacoraDTO
    {
        public int Id { get; set; }

        public string Observacion {get; set; }

         public string Usuario { get; set; }

         public int Id_Accion {get;set;}

         public int Id_Memorandum {get; set;}

         public string Fecha_Bitacora {get;set;}

         public int Id_Estado {get;set;}

         public EstadoDTO Estado { get; set; }

         public MemorandumDTO Memorandum { get; set; }
    }
}