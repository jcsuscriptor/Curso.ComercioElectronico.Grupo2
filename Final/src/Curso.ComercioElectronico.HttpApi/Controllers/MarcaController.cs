

using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
public class MarcaController : ControllerBase
{

    private readonly IMarcaAppService marcaAppService;

    public MarcaController(IMarcaAppService marcaAppService)
    {
        this.marcaAppService = marcaAppService;
    }

    [HttpGet]
    public async Task<ICollection<MarcaDto>> GetAllAsync()
    {

        return await marcaAppService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<MarcaDto> GetByIdAsync(string id)
    {
        return await marcaAppService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<MarcaDto> CreateAsync(MarcaCrearDto marca)
    {

        return await marcaAppService.CreateAsync(marca);

    }

    [HttpPut]
    public async Task UpdateAsync(string id, MarcaActualizarDto marca)
    {

        await marcaAppService.UpdateAsync(id, marca);

    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(string marcaId)
    {

        return await marcaAppService.DeleteAsync(marcaId);

    }

}