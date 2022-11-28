using AutoMapper;
using Curso.ComercioElectronico.Domain;
using FluentValidation;

namespace Curso.ComercioElectronico.Application;

public class ClienteCategoriaAppService :
    BaseAppService<ClienteCategoria, ClienteCategoriaDto, string,
        ClienteCategoriaCrearDto, ClienteCategoriaActualizarDto>,
    IClienteCategoriaAppService
{
    public ClienteCategoriaAppService(
        IClienteCategoriaRepository repository, 
        IUnitOfWork unitOfWork, 
        IValidator<ClienteCategoriaCrearDto> validatorCrear,
        IValidator<ClienteCategoriaActualizarDto> validatorActualizar, 
        IMapper mapper) : base(repository, unitOfWork, validatorCrear, validatorActualizar, mapper)
    {
    }

    
}
 