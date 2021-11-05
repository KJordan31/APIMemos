using Aplicacion;
using Aplicacion.Acciones;
using Aplicacion.Estados;

namespace Infraestructura.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        public UnitOfWork(IAccionRepository accionRepository,IEstadoRepository estadoRepository )
        {
            Acciones = accionRepository;         
            Estados = estadoRepository;   
        }

        public IAccionRepository Acciones { get; }
        public IEstadoRepository Estados { get; }
    }
}