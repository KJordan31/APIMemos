using Aplicacion;
using Aplicacion.Acciones;
using Aplicacion.Destinatarios;
using Aplicacion.Estados;
using Aplicacion.Tipos;
using Infraestructura.Repository;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddTransient<IUnitOfWork, UnitOfWork>();

        }
    }
}