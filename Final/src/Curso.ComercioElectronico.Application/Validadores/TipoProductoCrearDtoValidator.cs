using Curso.ComercioElectronico.Domain;
using FluentValidation;

namespace Curso.ComercioElectronico.Application;

public class TipoProductoCrearDtoValidator : AbstractValidator<TipoProductoCrearDto>
{
    private readonly ITipoProductoRepository repository;

    public TipoProductoCrearDtoValidator(ITipoProductoRepository repository)
    {
        this.repository = repository;

        RuleFor(entidadDto => entidadDto)
           .Must((entidadDto, propiedad, context) => ValidarExistencia(entidadDto, context))
           .WithMessage("Ya existe una entidad con el id: {id}");

    }

    public bool ValidarExistencia(TipoProductoCrearDto entidadCrearDto,
        ValidationContext<TipoProductoCrearDto> context)
    {
        context.MessageFormatter.AppendArgument("id", entidadCrearDto.Id);

        var taskExistencia = repository.GetByIdAsync(entidadCrearDto.Id);
        var entidadExiste = taskExistencia.Result;

        if (entidadExiste != null)
        {
            return false;
        }


        return true;
    }
}
