using Aplicacion;
using Aplicacion.Acciones;
using Aplicacion.Destinatarios;
using Aplicacion.Estados;
using Aplicacion.Tipos;
using Aplicacion.Memos;
using Infraestructura.Repository;
using Microsoft.Extensions.DependencyInjection;
using Aplicacion.Contenidos;

namespace Infraestructura
{
    public static class ServiceRegistration
    {
        public static void AddInfraestructure(this IServiceCollection services)
        {
            services.AddTransient<IAccionRepository, AccionRepository>();
            services.AddTransient<IEstadoRepository, EstadoRepository>();
            services.AddTransient<ITipoMemorandumRepository, TipoMemorandumRepository>();
            services.AddTransient<ITipoDestinatarioRepository, TipoDestinatarioRepository>();
            services.AddTransient<IMemorandumRepository, MemorandumRepository>();
            services.AddTransient<IContenidoRepository, ContenidoRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

        }
    }
}