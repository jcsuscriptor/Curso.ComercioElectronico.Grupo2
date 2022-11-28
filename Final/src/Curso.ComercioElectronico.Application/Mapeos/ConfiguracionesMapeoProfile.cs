

using AutoMapper;
using Curso.ComercioElectronico.Application;
using Curso.ComercioElectronico.Domain;

public class ConfiguracionesMapeoProfile : Profile
{
     
    public ConfiguracionesMapeoProfile()
    {
        CreateMap<TipoProductoCrearDto, TipoProducto>();
        CreateMap<TipoProductoActualizarDto, TipoProducto>();
        CreateMap<TipoProducto, TipoProductoDto>();

        CreateMap<MarcaCrearDto, Marca>();
        CreateMap<MarcaActualizarDto, Marca>();
        CreateMap<Marca, MarcaDto>();

        CreateMap<ProductoCrearActualizarDto, Producto>();



        //Cliente
        CreateMap<ClienteCategoriaCrearDto, ClienteCategoria>() 
           ;
        CreateMap<ClienteCategoriaActualizarDto, ClienteCategoria>()
          ;


        CreateMap<ClienteCategoria, ClienteCategoriaDto>()
           ;

        CreateMap<ClienteCrearActualizarDto, Cliente>()
            .ConstructUsing((src, des) => new Cliente(Guid.NewGuid()))
            ;


        CreateMap<UsuarioCrearDto, Usuario>()
            .ConstructUsing((src, des) => new Usuario(Guid.NewGuid()))
            ;

    }
}

