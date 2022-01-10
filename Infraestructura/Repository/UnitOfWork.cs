using Aplicacion;
using Aplicacion.Acciones;
using Aplicacion.Destinatarios;
using Aplicacion.Estados;
using Aplicacion.Tipos;
using Aplicacion.Memos;
using Aplicacion.Contenidos;
using Apliacion.Adjuntos;

namespace Infraestructura.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        public UnitOfWork(IAccionRepository accionRepository,IEstadoRepository estadoRepository, 
        ITipoMemorandumRepository tipoMemorandumRepository, 
        ITipoDestinatarioRepository tipoDestinatarioRepository, IMemorandumRepository memorandumRepository,
        IContenidoRepository contenidoRepository, IAdjuntoRepository adjuntoRepository)
        {
            Acciones = accionRepository;         
            Estados = estadoRepository;
            Tipos = tipoMemorandumRepository; 
            Destinatarios = tipoDestinatarioRepository;  
            Memos= memorandumRepository; 
            Contenidos = contenidoRepository;
            Adjuntos = adjuntoRepository; 
        }

        public IAccionRepository Acciones { get; }
        
        public IEstadoRepository Estados { get; }

        public ITipoMemorandumRepository Tipos {get; }

        public ITipoDestinatarioRepository Destinatarios {get; }

        public IMemorandumRepository Memos {get; }

        public IContenidoRepository Contenidos {get; }

        public IAdjuntoRepository Adjuntos {get; }
    }
}