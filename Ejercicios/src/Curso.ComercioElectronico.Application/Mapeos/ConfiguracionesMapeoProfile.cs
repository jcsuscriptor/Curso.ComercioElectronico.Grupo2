

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


    }
}

