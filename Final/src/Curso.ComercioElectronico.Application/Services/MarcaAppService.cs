using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Curso.ComercioElectronico.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;



public class MarcaAppService : IMarcaAppService
{
    private readonly IMarcaRepository repository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IValidator<MarcaCrearDto> validatorCrear;
    private readonly IValidator<MarcaActualizarDto> validatorActualizar;
    private readonly IMapper mapper;
    private readonly ILogger<MarcaAppService> logger;

    public MarcaAppService(IMarcaRepository repository, 
        IUnitOfWork unitOfWork,
        IValidator<MarcaCrearDto> validatorCrear,
        IValidator<MarcaActualizarDto> validatorActualizar,
        IMapper mapper,
        ILogger<MarcaAppService> logger)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.validatorCrear = validatorCrear;
        this.validatorActualizar = validatorActualizar;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<MarcaDto> CreateAsync(MarcaCrearDto marcaDto)
    {
        logger.LogInformation("Crear Marca");

        //Reglas Validaciones... 
        await validatorCrear.ValidateAndThrowAsync(marcaDto);
 
        var existeNombreMarca = await repository.ExisteNombre(marcaDto.Nombre);
        if (existeNombreMarca){
            
            var msg = $"Ya existe una marca con el nombre {marcaDto.Nombre}";
            logger.LogError(msg);

            throw new ArgumentException(msg);
        }

        var marca = mapper.Map<Marca>(marcaDto);
 
        //Persistencia objeto
        marca = await repository.AddAsync(marca);
        await unitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        var marcaCreada = mapper.Map<MarcaDto>(marca);
        return marcaCreada; 
       
    }

    public async Task UpdateAsync(string id, MarcaActualizarDto marcaDto)
    {
        //Reglas Validaciones... 
        await validatorActualizar.ValidateAndThrowAsync(marcaDto);


        var marca = await repository.GetByIdAsync(id);
        if (marca == null){
            throw new NotFoundException($"La marca con el id: {id}, no existe");
        }
        
        var existeNombreMarca = await repository.ExisteNombre(marcaDto.Nombre,id);
        if (existeNombreMarca){
            throw new BusinessException($"Ya existe una marca con el nombre {marcaDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        marca.Nombre = marcaDto.Nombre;

        //Persistencia objeto
        await repository.UpdateAsync(marca);
        await unitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(string marcaId)
    {
        //Reglas Validaciones... 
        var marca = await repository.GetByIdAsync(marcaId);
        if (marca == null){
            throw new NotFoundException($"La marca con el id: {marcaId}, no existe");
        }

        await repository.DeleteAsync(marca);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<ICollection<MarcaDto>> GetAllAsync()
    {
        var marcaList = await repository.GetAllAsync();

        var marcaListDto =  from m in marcaList
                            select new MarcaDto(){
                                Id = m.Id,
                                Nombre = m.Nombre
                            };

        return marcaListDto.ToList();
    }

    public async Task<MarcaDto> GetByIdAsync(string id)
    {
        var entidad = await repository.GetByIdAsync(id);

        var entidadDto = mapper.Map<MarcaDto>(entidad);
        return entidadDto;
    }
}
 