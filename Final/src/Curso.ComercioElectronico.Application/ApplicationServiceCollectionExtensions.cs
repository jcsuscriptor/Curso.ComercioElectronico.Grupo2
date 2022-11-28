using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Curso.ComercioElectronico.Application;


public static class ApplicationServiceCollectionExtensions
{

    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {

        services.AddTransient<IMarcaAppService, MarcaAppService>();
        services.AddTransient<ITipoProductoAppService, TipoProductoAppService>();
        services.AddTransient<IProductoAppService, ProductoAppService>();
        services.AddTransient<IOrdenAppService, OrdenAppService>();
        services.AddTransient<IClienteAppService, ClienteAppService>();
        services.AddTransient<IClienteCategoriaAppService, ClienteCategoriaAppService>();
        services.AddTransient<IUsuarioAppService, UsuarioAppService>();

        //Configurar la inyección de todos los profile que existen en un Assembly
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //Opcion 1: Configurar los validaciones, individualmente
        services.AddScoped<IValidator<MarcaCrearDto>, 
                        MarcaCrearDtoValidator>();

        services.AddScoped<IValidator<MarcaActualizarDto>,
                        MarcaActualizarDtoValidator>();

        services.AddScoped<IValidator<MarcaActualizarDto>,
                        MarcaActualizarDtoValidator>();

        services.AddScoped<IValidator<ProductoCrearActualizarDto>,
                      ProductoCrearActualizarDtoValidator>();

        services.AddScoped<IValidator<ProductoCrearActualizarDto>,
                      ProductoCrearActualizarDtoValidator>();

        services.AddScoped<IValidator<TipoProductoCrearDto>,
                     TipoProductoCrearDtoValidator>();

        services.AddScoped<IValidator<TipoProductoActualizarDto>,
                    TipoProductoActualizarDtoValidator>();
 
        services.AddScoped<IValidator<ClienteCategoriaCrearDto>,
                 ClienteCategoriaCrearDtoValidator>();

        services.AddScoped<IValidator<ClienteCategoriaActualizarDto>,
                ClienteCategoriaActualizarDtoValidator>();

        services.AddScoped<IValidator<ClienteCrearActualizarDto>,
                   ClienteCrearActualizarDtoValidator>();

        services.AddScoped<IValidator<UsuarioCrearDto>,
                   UsuarioCrearDtoValidator>();

        //Opcion 2. Configurar todas las validaciones de Assembly
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;

    }
}