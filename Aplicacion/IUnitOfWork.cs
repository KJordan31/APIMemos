using Aplicacion.Acciones;
using Aplicacion.Destinatarios;
using Aplicacion.Estados;
using Aplicacion.Memos;
using Aplicacion.Tipos;

namespace Aplicacion
{
    public interface IUnitOfWork
    {
         IAccionRepository Acciones { get; }
         IEstadoRepository Estados { get; }

         ITipoMemorandumRepository Tipos {get; }

         ITipoDestinatarioRepository Destinatarios {get; }
        IMemorandumRepository Memos {get; }
    }
}