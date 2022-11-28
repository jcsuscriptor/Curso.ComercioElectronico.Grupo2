

using Curso.ComercioElectronico.Application;
using Curso.ComercioElectronico.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.HttpApi.Controllers;


[ApiController]
[Route("[controller]")]
public class ClienteCategoriaController : BaseController<ClienteCategoriaDto, string,
    ClienteCategoriaCrearDto, ClienteCategoriaActualizarDto>
{
    public ClienteCategoriaController(
        IClienteCategoriaAppService appService)
        : base(appService)
    {
    }
}