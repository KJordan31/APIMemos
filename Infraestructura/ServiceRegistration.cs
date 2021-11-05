using Aplicacion;
using Aplicacion.Acciones;
using Aplicacion.Estados;
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
            services.AddTransient<IUnitOfWork, UnitOfWork>();

        }
    }
}