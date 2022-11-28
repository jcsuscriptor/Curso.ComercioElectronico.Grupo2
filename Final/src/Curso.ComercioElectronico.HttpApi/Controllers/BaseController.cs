

using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


public abstract class BaseController<TEntityDto> :
        BaseController<TEntityDto, int>
    where TEntityDto : class
{
    protected BaseController(
        IBaseAppService<TEntityDto, int> appService) 
        : base(appService)
    {
    }
}

public abstract class BaseController< TEntityDto, TEntityId> :
     BaseController<TEntityDto, TEntityId, TEntityDto>
     where TEntityDto : class 
{
    protected BaseController(IBaseAppService<TEntityDto, TEntityId, TEntityDto>
        appService) : base(appService)
    {
    }
}

public abstract class BaseController< TEntityDto, TEntityId, TEntityCreateDto> :
     BaseController<TEntityDto, TEntityId, TEntityCreateDto, TEntityCreateDto>
     where TEntityDto : class
     where TEntityCreateDto : class
{
    protected BaseController(IBaseAppService<TEntityDto, TEntityId, TEntityCreateDto>
        appService) : base(appService)
    {
    }
}


public abstract class BaseController< TEntityDto, TEntityId, TEntityCreateDto, TEntityUpdateDto> : ControllerBase
     where TEntityDto : class
     where TEntityCreateDto : class
     where TEntityUpdateDto : class
{
    private readonly IBaseAppService<TEntityDto, TEntityId, TEntityCreateDto, TEntityUpdateDto> appService;

    public BaseController(IBaseAppService<TEntityDto, TEntityId, TEntityCreateDto, TEntityUpdateDto> appService)
    {
        this.appService = appService;
    }

    [HttpGet]
    public async Task<ICollection<TEntityDto>> GetAllAsync()
    {

        return await appService.GetAllAsync();
    }

    [HttpPost]
    public async Task<TEntityDto> CreateAsync(TEntityCreateDto entidadCrearDto)
    {

        return await appService.CreateAsync(entidadCrearDto);

    }

    [HttpPut]
    public async Task UpdateAsync(TEntityId id, TEntityUpdateDto entidadActualizarDto)
    {

        await appService.UpdateAsync(id, entidadActualizarDto);

    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(TEntityId marcaId)
    {

        return await appService.DeleteAsync(marcaId);

    }

}