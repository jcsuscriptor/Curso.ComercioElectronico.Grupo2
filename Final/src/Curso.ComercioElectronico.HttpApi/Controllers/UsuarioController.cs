

using Curso.ComercioElectronico.Application;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{

    private readonly IUsuarioAppService usuarioAppService;

    public UsuarioController(IUsuarioAppService usuarioAppService)
    {
        this.usuarioAppService = usuarioAppService;
    }
     

    [HttpGet]
    public Task<ListaPaginada<UsuarioDto>> GetListAsync([FromQuery] UsuarioListInput input)
    {
        return usuarioAppService.GetListAsync(input);
    }

    [HttpGet("{id}")]
    public async Task<UsuarioDto> GetByIdAsync(Guid id)
    {
        return await usuarioAppService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<UsuarioDto> CreateAsync(UsuarioCrearDto usuarioCrear)
    {

        return await usuarioAppService.CreateAsync(usuarioCrear);

    }

    [HttpPut]
    public async Task ActiveAsync(Guid entidadId, bool active)
    {

        await usuarioAppService.ActiveAsync(entidadId, active);

    }

    [HttpDelete]
    public async Task<bool> DeleteAsync(Guid id)
    {

        return await usuarioAppService.DeleteAsync(id);

    }


    [HttpGet("fromUser/{user}")]
    public async Task<UsuarioDto> GetByUserAsync(string user)
    {
        return await usuarioAppService.GetByUserAsync(user);
    }

    [HttpPut("changePassword")]
    public async Task ChangePasswordAsync(Guid entidadId, string passwordNew)
    {
         await usuarioAppService.ChangePasswordAsync(entidadId, passwordNew);
    }


}