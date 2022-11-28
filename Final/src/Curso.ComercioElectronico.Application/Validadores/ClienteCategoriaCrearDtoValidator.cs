using Curso.ComercioElectronico.Domain;
using FluentValidation;

namespace Curso.ComercioElectronico.Application;

public class ClienteCategoriaCrearDtoValidator : AbstractValidator<ClienteCategoriaCrearDto>
{
    private readonly IClienteCategoriaRepository repository;

    public ClienteCategoriaCrearDtoValidator(IClienteCategoriaRepository repository)
    {
        this.repository = repository;

        RuleFor(clientCategoria => clientCategoria)
              .Must((clientCategoria, propiedad, context) => ValidarId(clientCategoria,  context))
              .WithMessage("Ya existe una categoria con el identificador {id}");

    }

    public bool ValidarId(ClienteCategoriaCrearDto clientCategoria,
        ValidationContext<ClienteCategoriaCrearDto> context)
    {
        context.MessageFormatter.AppendArgument("id", clientCategoria.Id);

        var taskClienteCategoria = repository.GetByIdAsync(clientCategoria.Id);
        var categoriaExistente = taskClienteCategoria.Result;
 
        if (categoriaExistente!=null)
        {
            return false;
        }
         

        return true;
    }


}
