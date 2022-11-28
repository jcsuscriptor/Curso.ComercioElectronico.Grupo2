

using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{

    private readonly IClienteAppService clienteAppService;

    public ClienteController(IClienteAppService clienteAppService)
    {
        this.clienteAppService = clienteAppService;
    }
     

    [HttpGet]
    public Task<ListaPaginada<ClienteDto>> GetListAsync(string? buscar, int limit = 10, int offset = 0,
        string? categoriaId = null)
    {
        return clienteAppService.GetListAsync(buscar,limit,offset,categoriaId);
    }

    [HttpGet("{id}")]
    public async Task<ClienteDto> GetByIdAsync(Guid id)
    {
        return await clienteAppService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<ClienteDto> CreateAsync(ClienteCrearActualizarDto clienteCrearActualizarDto)
    {

        return await clienteAppService.CreateAsync(clienteCrearActualizarDto);

    }

    [HttpPut]
    public async Task UpdateAsync(Guid id, ClienteCrearActualizarDto entidadDto)
    {

        await clienteAppService.UpdateAsync(id, entidadDto);

    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(Guid id)
    {

        return await clienteAppService.DeleteAsync(id);

    }


    [HttpGet("fromUser/{usuarioId}")]
    public async Task<ClienteDto> GetByUserIdAsync(Guid usuarioId)
    {
        return await clienteAppService.GetByUserIdAsync(usuarioId);
    }


}