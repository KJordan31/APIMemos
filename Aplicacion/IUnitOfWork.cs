using Aplicacion.Acciones;
using Aplicacion.Adjuntos;
using Aplicacion.Bitacoras;
using Aplicacion.Contenidos;
using Aplicacion.Destinatarios;
using Aplicacion.DestinatariosUsu;
using Aplicacion.Estados;
using Aplicacion.Firmas;
using Aplicacion.Memos;
using Aplicacion.Tipos;
using Aplicacion.Usuarios;

namespace Aplicacion
{
    public interface IUnitOfWork
    {
        IAccionRepository Acciones { get; }

        IEstadoRepository Estados { get; }

        ITipoMemorandumRepository Tipos { get; }

        ITipoDestinatarioRepository Destinatarios { get; }

        IMemorandumRepository Memos { get; }

        IContenidoRepository Contenidos { get; }

        IAdjuntoRepository Adjuntos { get; }

        IBitacoraRepository Bitacoras { get; }

        IDestinatarioRepository DestinatariosUsu { get; }

        IFirmaRepository Firmas { get; }

        IUsuarioRepository Usuarios { get; }
    }
}
