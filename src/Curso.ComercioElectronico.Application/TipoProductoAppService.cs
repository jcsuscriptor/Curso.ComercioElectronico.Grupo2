using AutoMapper;
using Curso.ComercioElectronico.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;

public class TipoProductoAppService : ITipoProductoAppService
{
    private readonly ITipoProductoRepository tipoProductoRepository;
    private readonly IValidator<TipoProductoCrearDto> validatorCrear;
    private readonly IValidator<TipoProductoActualizarDto> validatorActualizar;
    private readonly IMapper mapper;
    private readonly ILogger<TipoProductoAppService> logger;

    public TipoProductoAppService(ITipoProductoRepository tipoProductoRepository,
        IValidator<TipoProductoCrearDto> validatorCrear,
        IValidator<TipoProductoActualizarDto> validatorActualizar,
        IMapper mapper,
        ILogger<TipoProductoAppService> logger )
    {
        this.tipoProductoRepository = tipoProductoRepository;
        this.validatorCrear = validatorCrear;
        this.validatorActualizar = validatorActualizar;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<TipoProductoDto> CreateAsync(TipoProductoCrearDto tipoProductoDto)
    {
       
        logger.LogInformation("Crear Tipo Producto");

        //Reglas Validaciones... 
        await validatorCrear.ValidateAndThrowAsync(tipoProductoDto);


        var tipoProducto = mapper.Map<TipoProducto>(tipoProductoDto);

        //Persistencia objeto
        tipoProducto = await tipoProductoRepository.AddAsync(tipoProducto);
        await tipoProductoRepository.UnitOfWork.SaveChangesAsync();
      
        var tipoProductoCreada = mapper.Map<TipoProductoDto>(tipoProducto);
        return tipoProductoCreada;
    }

    public async Task UpdateAsync(string id, TipoProductoActualizarDto tipoProductoDto)
    {
        //Reglas Validaciones... 
        await validatorActualizar.ValidateAndThrowAsync(tipoProductoDto);


        var tipoProducto = await tipoProductoRepository.GetByIdAsync(id);
        if (tipoProducto == null)
        {
            throw new ArgumentException($"El tipo de producto con el id: {id}, no existe");
        }

        var existeNombre = await tipoProductoRepository.ExisteNombre(tipoProducto.Nombre, id);
        if (existeNombre)
        {
            throw new ArgumentException($"Ya existe tipo de producto con el nombre {tipoProducto.Nombre}");
        }

        //Mapeo Dto => Entidad
        tipoProducto.Nombre = tipoProductoDto.Nombre;

        //Persistencia objeto
        await tipoProductoRepository.UpdateAsync(tipoProducto);
        await tipoProductoRepository.UnitOfWork.SaveChangesAsync();

        return;
    }


    public async Task<bool> DeleteAsync(string id)
    {
        //Reglas Validaciones... 
        var entidad = await tipoProductoRepository.GetByIdAsync(id);
        if (entidad == null)
        {
            throw new ArgumentException($"La entidad con el id: {id}, no existe");
        }

        await tipoProductoRepository.DeleteAsync(entidad);
        await tipoProductoRepository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<ICollection<TipoProductoDto>> GetAllAsync()
    {
        var tiposProductos = await tipoProductoRepository.GetAllAsync();

        return tiposProductos.Select(a => new TipoProductoDto()
        {
            Id = a.Id,
            Nombre = a.Nombre
        }).ToList();
    }

  
    public async Task<TipoProductoDto> GetByIdAsync(string id)
    {
        var tipoProducto = await tipoProductoRepository.GetByIdAsync(id);

        var tipoProductoDto = mapper.Map<TipoProductoDto>(tipoProducto);
        return tipoProductoDto;
    }

    
}

