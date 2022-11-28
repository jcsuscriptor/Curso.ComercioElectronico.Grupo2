using Curso.ComercioElectronico.Domain;
using FluentValidation;

namespace Curso.ComercioElectronico.Application;

public class MarcaCrearDtoValidator : AbstractValidator<MarcaCrearDto>
{
    private readonly IMarcaRepository repository;

    public MarcaCrearDtoValidator(IMarcaRepository repository)
    {
        this.repository = repository;

        RuleFor(entidadDto => entidadDto)
           .Must((entidadDto, propiedad, context) => ValidarExistencia(entidadDto, context))
           .WithMessage("Ya existe una entidad con el id: {id}");

    }

    public bool ValidarExistencia(MarcaCrearDto entidadCrearDto,
        ValidationContext<MarcaCrearDto> context)
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
