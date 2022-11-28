using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Curso.ComercioElectronico.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;

public abstract class BaseAppService<TEntity, TEntityDto> :
        BaseAppService<TEntity, TEntityDto, int, TEntityDto, TEntityDto>,
        IBaseAppService<TEntityDto>
        where TEntity : class
        where TEntityDto : class
{
    public BaseAppService(IRepository<TEntity, int> 
        repository, IUnitOfWork unitOfWork, 
        IValidator<TEntityDto> validatorCrear, 
        IValidator<TEntityDto> validatorActualizar,
        IMapper mapper) : base(repository, unitOfWork, validatorCrear, validatorActualizar, mapper)
    {
    }
}

public abstract class BaseAppService<TEntity, TEntityDto, TEntityId> :
        BaseAppService<TEntity, TEntityDto, TEntityId, TEntityDto, TEntityDto>,
        IBaseAppService<TEntityDto, TEntityId>
        where TEntity : class
        where TEntityDto : class
{
    public BaseAppService(
        IRepository<TEntity, TEntityId> repository, 
        IUnitOfWork unitOfWork, 
        IValidator<TEntityDto> validatorCrear, 
        IValidator<TEntityDto> validatorActualizar, 
        IMapper mapper) :
        base(repository, unitOfWork, validatorCrear, validatorActualizar, mapper)
    {
    }
}

public abstract class BaseAppService<TEntity, TEntityDto, TEntityId, TEntityCreateUpdateDto> :
        BaseAppService<TEntity, TEntityDto, TEntityId, TEntityCreateUpdateDto, TEntityCreateUpdateDto>,
        IBaseAppService<TEntityDto, TEntityId, TEntityCreateUpdateDto>
        where TEntity : class
        where TEntityDto : class
        where TEntityCreateUpdateDto : class
{
    public BaseAppService(IRepository<TEntity, TEntityId> repository, IUnitOfWork unitOfWork,
        IValidator<TEntityCreateUpdateDto> validatorCrear, 
        IValidator<TEntityCreateUpdateDto> validatorActualizar, IMapper mapper) 
        : base(repository, unitOfWork, validatorCrear, validatorActualizar, mapper)
    {
    }
}


public abstract class BaseAppService<TEntity,TEntityDto, TEntityId, TEntityCreateDto, TEntityUpdateDto> :
        IBaseAppService<TEntityDto, TEntityId, TEntityCreateDto, TEntityUpdateDto>
        where TEntity : class
        where TEntityDto : class
        where TEntityCreateDto : class
        where TEntityUpdateDto : class
{
    private readonly IRepository<TEntity, TEntityId> repository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IValidator<TEntityCreateDto> validatorCrear;
    private readonly IValidator<TEntityUpdateDto> validatorActualizar;
    private readonly IMapper mapper;

    public BaseAppService(IRepository<TEntity, TEntityId> repository, 
        IUnitOfWork unitOfWork,
        IValidator<TEntityCreateDto> validatorCrear,
        IValidator<TEntityUpdateDto> validatorActualizar,
        IMapper mapper)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.validatorCrear = validatorCrear;
        this.validatorActualizar = validatorActualizar;
        this.mapper = mapper; 
    }

    public virtual async Task<TEntityDto> CreateAsync(TEntityCreateDto entidadCreateDto)
    {
        //Reglas Validaciones... 
        await validatorCrear.ValidateAndThrowAsync(entidadCreateDto);

        var entidad = mapper.Map<TEntity>(entidadCreateDto);

        //Persistencia objeto
        entidad = await repository.AddAsync(entidad);
        await unitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        var entidadCreadaDto = mapper.Map<TEntityDto>(entidad);
        return entidadCreadaDto;  
    }

    public async Task UpdateAsync(TEntityId id, TEntityUpdateDto entidadUpdateDto)
    {
        //Reglas Validaciones... 
        await validatorActualizar.ValidateAndThrowAsync(entidadUpdateDto);


        var entidad = await repository.GetByIdAsync(id);
        if (entidad == null){
            throw new NotFoundException($"La entidad con el id: {id}, no existe");
        }
         
        entidad = mapper.Map(entidadUpdateDto,entidad);

        //Persistencia objeto
        await repository.UpdateAsync(entidad);
        await unitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(TEntityId id)
    {
          
        var entidad = await repository.GetByIdAsync(id);
        if (entidad == null)
        {
            throw new NotFoundException($"La entidad con el id: {id}, no existe");
        }


        await repository.DeleteAsync(entidad);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<ICollection<TEntityDto>> GetAllAsync()
    {
        var entidadList = await repository.GetAllAsync();

        var entidadDtoList =  mapper.Map<ICollection<TEntityDto>>(entidadList);

        return entidadDtoList;
    }

    public async Task<TEntityDto> GetByIdAsync(TEntityId id)
    {
        var entidad = await repository.GetByIdAsync(id);

        var entidadDto = mapper.Map<TEntityDto>(entidad);
        return entidadDto;
    }
}
 