namespace Curso.ComercioElectronico.Application;

public interface IBaseAppService<TEntityDto> :
    IBaseAppService<TEntityDto, int>
    where TEntityDto : class
{

}

public interface IBaseAppService<TEntityDto, TEntityId> :
    IBaseAppService<TEntityDto, TEntityId, TEntityDto>
    where TEntityDto : class
{

}

public interface IBaseAppService<TEntityDto, TEntityId, TEntityCreateUpdateDto>
    : IBaseAppService<TEntityDto, TEntityId, TEntityCreateUpdateDto, TEntityCreateUpdateDto>
       where TEntityDto : class
        where TEntityCreateUpdateDto : class
{

}


public interface IBaseAppService<TEntityDto, TEntityId, TEntityCreateDto, TEntityUpdateDto> where
        TEntityDto : class
        where TEntityCreateDto : class
        where TEntityUpdateDto : class
{

    Task<TEntityDto> GetByIdAsync(TEntityId id);

    Task<ICollection<TEntityDto>> GetAllAsync();

    Task<TEntityDto> CreateAsync(TEntityCreateDto entityDto);

    Task UpdateAsync(TEntityId id, TEntityUpdateDto entityDto);

    Task<bool> DeleteAsync(TEntityId id);
}
