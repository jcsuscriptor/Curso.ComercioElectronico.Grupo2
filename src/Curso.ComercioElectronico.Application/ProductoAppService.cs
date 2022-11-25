using AutoMapper;
using Curso.ComercioElectronico.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;

public class ProductoAppService : IProductoAppService
{
    private readonly IProductoRepository productoRepository;
    private readonly IMarcaRepository marcaRepository;
    private readonly ITipoProductoRepository tipoProductoRepository;
    private readonly IValidator<ProductoCrearActualizarDto> validator;
    private readonly IMapper mapper;
    private readonly ILogger<ProductoAppService> logger;

    public ProductoAppService(IProductoRepository productoRepository,
            IMarcaRepository marcaRepository,
            ITipoProductoRepository tipoProductoRepository,
            IValidator<ProductoCrearActualizarDto> validator,
            IMapper mapper,
            ILogger<ProductoAppService> logger)
    {
        this.productoRepository = productoRepository;
        this.marcaRepository = marcaRepository;
        this.tipoProductoRepository = tipoProductoRepository;
        this.validator = validator;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<ProductoDto> CreateAsync(ProductoCrearActualizarDto productoDto)
    {

        logger.LogInformation("Crear Producto");

        //Reglas Validaciones... 
        await validator.ValidateAndThrowAsync(productoDto);


        //Mapeo Dto => Entidad
        var producto = mapper.Map<Producto>(productoDto);
 
        //Persistencia objeto
        producto = await productoRepository.AddAsync(producto);
        await productoRepository.UnitOfWork.SaveChangesAsync();

        return await GetByIdAsync(producto.Id);
    }

    

    public async Task<ListaPaginada<ProductoDto>> GetListAsync(ProductoListInput input){

        var consulta = productoRepository.GetAllIncluding(x => x.Marca,
                            x => x.TipoProducto);
  
        //Aplicar filtros
        if (!string.IsNullOrEmpty(input.TipoProductoId)){
          consulta = consulta.Where(x => x.TipoProductoId == input.TipoProductoId);
        }

        if (!string.IsNullOrEmpty(input.MarcaId)){
          consulta = consulta.Where(x => x.MarcaId == input.MarcaId);
        }

        if (!string.IsNullOrEmpty(input.ValorBuscar)){

            //consulta = consulta.Where(x => x.Nombre.Contains(input.ValorBuscar) ||
            //    x.Codigo.StartsWith(input.ValorBuscar));
            consulta = consulta.Where(x => x.Nombre.Contains(input.ValorBuscar));
        }

        //Ejecuatar linq. Total registros
        var total = consulta.Count();
     
        //Aplicar paginacion
        consulta = consulta.Skip(input.Offset)
                    .Take(input.Limit);
       
        //Obtener el listado paginado. (Proyeccion)
        var consulaListaProductosDto = consulta
                                .Select(
                                    x => new ProductoDto()
                                    {
                                        Id = x.Id,
                                        Caducidad = x.Caducidad,
                                        //Utilizar propiedad navegacion,
                                        // para obtener informacion de una clase relacionada
                                        Marca = x.Marca.Nombre,
                                        MarcaId = x.MarcaId,
                                        Nombre = x.Nombre,
                                        Observaciones = x.Observaciones,
                                        Precio = x.Precio,
                                        //Utilizar propiedad navegacion,
                                        // para obtener informacion de una clase relacionada
                                        TipoProducto = x.TipoProducto.Nombre,
                                        TipoProductoId = x.TipoProductoId
                                    }
                                );

       
        var resultado = new ListaPaginada<ProductoDto>();
        resultado.Total = total;
        resultado.Lista = await consulaListaProductosDto.ToAsyncEnumerable().ToListAsync();

        return resultado;

    }

    public Task<ProductoDto> GetByIdAsync(int id)
    {
        var consulta = productoRepository.GetAllIncluding(x => x.TipoProducto, x => x.Marca);
        consulta = consulta.Where(x => x.Id == id);

        var consultaProductoDto = consulta
                                .Select(
                                    x => new ProductoDto()
                                    {
                                        Id = x.Id,
                                        Caducidad = x.Caducidad,
                                        //Utilizar propiedad navegacion,
                                        // para obtener informacion de una clase relacionada
                                        Marca = x.Marca.Nombre,
                                        MarcaId = x.MarcaId,
                                        Nombre = x.Nombre,
                                        Observaciones = x.Observaciones,
                                        Precio = x.Precio,
                                        //Utilizar propiedad navegacion,
                                        // para obtener informacion de una clase relacionada
                                        TipoProducto = x.TipoProducto.Nombre,
                                        TipoProductoId = x.TipoProductoId
                                    }
                                );

        return Task.FromResult(consultaProductoDto.SingleOrDefault());
    }

    public async Task UpdateAsync(int id, ProductoCrearActualizarDto productoDto)
    {
        //Reglas Validaciones... 
        await validator.ValidateAndThrowAsync(productoDto);


        var producto = await productoRepository.GetByIdAsync(id);
        if (producto == null)
        {
            throw new ArgumentException($"La entidad con el id: {id}, no existe");
        }
         
        //Mapeo 
        producto = mapper.Map(productoDto,producto);
   
        //Persistencia objeto
        await productoRepository.UpdateAsync(producto);
        await productoRepository.UnitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        //Reglas Validaciones.

        var entidad = await productoRepository.GetByIdAsync(id);
        if (entidad == null)
        {
            throw new ArgumentException($"La entidad con el id: {id}, no existe");
        }

        await productoRepository.DeleteAsync(entidad);
        await productoRepository.UnitOfWork.SaveChangesAsync();

        return true;
    }

}

