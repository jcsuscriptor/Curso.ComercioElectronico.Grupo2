using AutoMapper;
using Curso.ComercioElectronico.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Curso.ComercioElectronico.Application;


public class UsuarioAppService : IUsuarioAppService
{
    private readonly IUsuarioRepository repository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IValidator<UsuarioCrearDto> validator;
    private readonly IMapper mapper;
    private readonly ILogger<UsuarioAppService> logger;

    public UsuarioAppService(IUsuarioRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<UsuarioCrearDto> validator,
        IMapper mapper,
        ILogger<UsuarioAppService> logger)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.validator = validator;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<UsuarioDto> CreateAsync(UsuarioCrearDto entidadDto)
    {
        //Reglas Validaciones... 
        await validator.ValidateAndThrowAsync(entidadDto);

        //Mapeo Dto => Entidad
        //TODO: Encriptar clave
        var entidad = mapper.Map<Usuario>(entidadDto);

        //Persistencia objeto
        entidad = await repository.AddAsync(entidad);
        await unitOfWork.SaveChangesAsync();

        return await GetByIdAsync(entidad.Id);
    }



    public async Task<ListaPaginada<UsuarioDto>> GetListAsync(UsuarioListInput input)
    {
        var consulta = repository.GetQueryable();

        //Aplicar filtros
        if (!string.IsNullOrEmpty(input.User))
        {
            consulta = consulta.Where(x => x.User.ToUpper()==input.User);
        }

        if (input.Active.HasValue) {
            consulta = consulta.Where(x => x.Activo == input.Active.Value);
        }
         
        //Ejecuatar linq. Total registros
        var total = consulta.Count();

        //Aplicar paginacion
        consulta = consulta.Skip(input.Offset)
                    .Take(input.Limit);

        //Obtener el listado paginado. (Proyeccion)
        var consulaListaDto = ConsultaDto(consulta);


        var resultado = new ListaPaginada<UsuarioDto>();
        resultado.Total = total;
        resultado.Lista = await consulaListaDto.ToAsyncEnumerable().ToListAsync();

        return resultado;
    }

    public async Task ActiveAsync(Guid entidadId, bool active)
    {
      

        var entidad = await repository.GetByIdAsync(entidadId);
        if (entidad == null)
        {
            throw new NotFoundException($"La entidad con el id: {entidadId}, no existe");
        }

        //Mapeo 
        entidad.Activo = active;

        //Persistencia objeto
        await repository.UpdateAsync(entidad);
        await unitOfWork.SaveChangesAsync();

        return;
    }


    public async Task<bool> DeleteAsync(Guid id)
    {
        var entidad = await repository.GetByIdAsync(id);
        if (entidad == null)
        {
            throw new NotFoundException($"La entidad con el id: {id}, no existe");
        }

        await repository.DeleteAsync(entidad);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<UsuarioDto> GetByIdAsync(Guid id)
    {
        var consulta = repository.GetQueryable();

        consulta = consulta.Where(a => a.Id == id);

        var consulaDto = ConsultaDto(consulta);

        var entidadDto = await consulaDto.ToAsyncEnumerable().SingleOrDefaultAsync();

        return entidadDto;
    }


     


    public async Task<UsuarioDto> GetByUserAsync(string user)
    {
        var consulta = repository.GetQueryable();

        consulta = consulta.Where(a => a.User.ToUpper() == user.ToUpper());

        var consulaDto = ConsultaDto(consulta);

        var entidadDto = await consulaDto.ToAsyncEnumerable().SingleOrDefaultAsync();

        return entidadDto;
    }


    private IQueryable<UsuarioDto> ConsultaDto(IQueryable<Usuario> consulta)
    {

        return consulta.Select(x => new UsuarioDto()
        {
            Id = x.Id, 
            User= x.User,
            Activo= x.Activo
        }
        );
    }

    

    public async Task ChangePasswordAsync(Guid entidadId, string passwordNew)
    {
        var entidad = await repository.GetByIdAsync(entidadId);

        if (entidad == null)
        {
            throw new NotFoundException($"La entidad con el id: {entidadId}, no existe");
        }

        //TODO: Encriptar clave 
        entidad.Clave = passwordNew;

        //Persistencia objeto
        await repository.UpdateAsync(entidad);
        await unitOfWork.SaveChangesAsync();

        return;

    }
}
