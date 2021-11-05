using Aplicacion.Acciones;
using Aplicacion.Estados;

namespace Aplicacion
{
    public interface IUnitOfWork
    {
         IAccionRepository Acciones { get; }
         IEstadoRepository Estados { get; }
    }
}