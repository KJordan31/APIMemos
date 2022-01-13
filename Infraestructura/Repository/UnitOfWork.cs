using Aplicacion;
using Aplicacion.Acciones;
using Aplicacion.Destinatarios;
using Aplicacion.Estados;
using Aplicacion.Tipos;
using Aplicacion.Memos;
using Aplicacion.Contenidos;
using Apliacion.Adjuntos;
using Aplicacion.Bitacoras;
using Aplicacion.DestinatariosUsu;

namespace Infraestructura.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        public UnitOfWork(IAccionRepository accionRepository,IEstadoRepository estadoRepository, 
        ITipoMemorandumRepository tipoMemorandumRepository, 
        ITipoDestinatarioRepository tipoDestinatarioRepository, IMemorandumRepository memorandumRepository,
        IContenidoRepository contenidoRepository, IAdjuntoRepository adjuntoRepository,
        IBitacoraRepository bitacoraRepository, IDestinatarioRepository destinatarioRepository)
        {
            Acciones = accionRepository;         
            Estados = estadoRepository;
            Tipos = tipoMemorandumRepository; 
            Destinatarios = tipoDestinatarioRepository;  
            Memos= memorandumRepository; 
            Contenidos = contenidoRepository;
            Adjuntos = adjuntoRepository; 
            Bitacoras = bitacoraRepository;
            DestinatariosUsu = destinatarioRepository;
        }

        public IAccionRepository Acciones { get; }
        
        public IEstadoRepository Estados { get; }

        public ITipoMemorandumRepository Tipos {get; }

        public ITipoDestinatarioRepository Destinatarios {get; }

        public IMemorandumRepository Memos {get; }

        public IContenidoRepository Contenidos {get; }

        public IAdjuntoRepository Adjuntos {get; }

        public IBitacoraRepository Bitacoras {get; }

        public IDestinatarioRepository DestinatariosUsu {get; }
    }
}