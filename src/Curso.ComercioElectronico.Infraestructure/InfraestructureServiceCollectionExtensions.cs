
using Curso.ComercioElectronico.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Curso.ComercioElectronico.Infraestructure;

public static class InfraestructureServiceCollectionExtensions {

    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration config) {

            services.AddTransient<IMarcaRepository, MarcaRepository>();
            services.AddTransient<IProductoRepository, ProductoRepository>(); 
            services.AddTransient<ITipoProductoRepository, TipoProductoRepository>(); 
            services.AddTransient<IOrdenRepository, OrdenRepository>(); 
            services.AddTransient<IClienteRepository, ClienteRepository>(); 

           
            return services;
 
     }
}