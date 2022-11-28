

using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
public class TipoProductoController : ControllerBase
{

    private readonly ITipoProductoAppService tipoProductoAppService;

    public TipoProductoController(ITipoProductoAppService tipoProductoAppService)
    {
        this.tipoProductoAppService = tipoProductoAppService;
    }

    [HttpGet]
    public async Task<ICollection<TipoProductoDto>> GetAllAsync()
    {

        return await tipoProductoAppService.GetAllAsync();
    }

    [HttpPost]
    public async Task<TipoProductoDto> CreateAsync(TipoProductoCrearDto marca)
    {

        return await tipoProductoAppService.CreateAsync(marca);

    }

    [HttpPut]
    public async Task UpdateAsync(string id, TipoProductoActualizarDto marca)
    {

        await tipoProductoAppService.UpdateAsync(id, marca);

    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(string marcaId)
    {

        return await tipoProductoAppService.DeleteAsync(marcaId);

    }

}