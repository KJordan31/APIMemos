using System.Threading.Tasks;
using Aplicacion;
using Aplicacion.Acciones;
using Aplicacion.Adjuntos;
using Aplicacion.Bitacoras;
using Aplicacion.Contenidos;
using Aplicacion.Destinatarios;
using Aplicacion.DestinatariosUsu;
using Aplicacion.Estados;
using Aplicacion.Firmas;
using Aplicacion.Memos;
using Aplicacion.Plantillas;
using Aplicacion.Tipos;
using Aplicacion.Usuarios;


namespace Infraestructura.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IAccionRepository accionRepository,
            IEstadoRepository estadoRepository,
            ITipoMemorandumRepository tipoMemorandumRepository,
            ITipoDestinatarioRepository tipoDestinatarioRepository,
            IMemorandumRepository memorandumRepository,
            IContenidoRepository contenidoRepository,
            IAdjuntoRepository adjuntoRepository,
            IBitacoraRepository bitacoraRepository,
            IDestinatarioRepository destinatarioRepository,
            IFirmaRepository firmaRepository,
            IUsuarioRepository usuarioRepository,
            IPlantillaRepository plantillaRepository
        )
        {
            Acciones = accionRepository;
            Estados = estadoRepository;
            Tipos = tipoMemorandumRepository;
            Destinatarios = tipoDestinatarioRepository;
            Memos = memorandumRepository;
            Contenidos = contenidoRepository;
            Adjuntos = adjuntoRepository;
            Bitacoras = bitacoraRepository;
            DestinatariosUsu = destinatarioRepository;
            Firmas = firmaRepository;
            Usuarios = usuarioRepository;
            Plantillas = plantillaRepository;
        }

        public IAccionRepository Acciones { get; }

        public IEstadoRepository Estados { get; }

        public ITipoMemorandumRepository Tipos { get; }

        public ITipoDestinatarioRepository Destinatarios { get; }

        public IMemorandumRepository Memos { get; }

        public IContenidoRepository Contenidos { get; }

        public IAdjuntoRepository Adjuntos { get; }

        public IBitacoraRepository Bitacoras { get; }

        public IDestinatarioRepository DestinatariosUsu { get; }

        public IFirmaRepository Firmas { get; }

        public IUsuarioRepository Usuarios { get; }

        public IPlantillaRepository Plantillas { get; }
    }
}
