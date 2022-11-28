using AutoMapper;
using Curso.ComercioElectronico.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;


public class ClienteAppService : IClienteAppService
{
    private readonly IClienteRepository repository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IValidator<ClienteCrearActualizarDto> validator;
    private readonly IMapper mapper;
    private readonly ILogger<ClienteAppService> logger;

    public ClienteAppService(IClienteRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<ClienteCrearActualizarDto> validator,
        IMapper mapper,
        ILogger<ClienteAppService> logger)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.validator = validator;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<ClienteDto> CreateAsync(ClienteCrearActualizarDto clienteDto)
    {
        //Reglas Validaciones... 
        await validator.ValidateAndThrowAsync(clienteDto);

        //Mapeo Dto => Entidad
        var entidad = mapper.Map<Cliente>(clienteDto);

        //Persistencia objeto
        entidad = await repository.AddAsync(entidad);
        await unitOfWork.SaveChangesAsync();

        return await GetByIdAsync(entidad.Id);
    }



    public async Task<ListaPaginada<ClienteDto>> GetListAsync(string? buscar, int limit = 10, int offset = 0,string? categoriaId=null)
    {
        var consulta = repository.GetAllIncluding(
                x => x.ClienteCategoria
            );

        //Aplicar filtros
        if (!string.IsNullOrEmpty(buscar))
        {
            consulta = consulta.Where(x => x.Nombres.Contains(buscar) ||
                x.Apellidos.Contains(buscar));
        }

        if (!string.IsNullOrEmpty(categoriaId)) {
            consulta = consulta.Where(x => x.ClienteCategoriaId == categoriaId);
        }
         
        //Ejecuatar linq. Total registros
        var total = consulta.Count();

        //Aplicar paginacion
        consulta = consulta.Skip(offset)
                    .Take(limit);

        //Obtener el listado paginado. (Proyeccion)
        var consulaListaDto = ConsultaDto(consulta);


        var resultado = new ListaPaginada<ClienteDto>();
        resultado.Total = total;
        resultado.Lista = await consulaListaDto.ToAsyncEnumerable().ToListAsync();

        return resultado;
    }

    public async Task UpdateAsync(Guid id, ClienteCrearActualizarDto entidadDto)
    {
        //Reglas Validaciones... 
        await validator.ValidateAndThrowAsync(entidadDto);


        var entidad = await repository.GetByIdAsync(id);
        if (entidad == null)
        {
            throw new NotFoundException($"La entidad con el id: {id}, no existe");
        }

        //Mapeo 
        entidad = mapper.Map(entidadDto, entidad);

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

    public async Task<ClienteDto> GetByIdAsync(Guid id)
    {
        var consulta = repository.GetAllIncluding(
               x => x.ClienteCategoria
           );

        consulta = consulta.Where(a => a.Id == id);

        var consulaDto = ConsultaDto(consulta);

        var entidadDto = await consulaDto.ToAsyncEnumerable().SingleOrDefaultAsync();

        return entidadDto;
    }




    public async Task<long?> DescuentoAsync(Guid clientId)
    {
        //Obtener categoria.
        var consulta = repository.GetAllIncluding(
              x => x.ClienteCategoria
          );

        consulta.Where(a => a.Id == clientId);

        var descuento  = 
            await consulta.Select(a => a.ClienteCategoria.Descuento).ToAsyncEnumerable().SingleOrDefaultAsync();

        return descuento;

    }


    public async Task<ClienteDto> GetByUserIdAsync(Guid usuarioId)
    {
        var consulta = repository.GetAllIncluding(
               x => x.ClienteCategoria
           );

        consulta = consulta.Where(a => a.UsuarioId == usuarioId);

        var consulaDto = ConsultaDto(consulta);

        var entidadDto = await consulaDto.ToAsyncEnumerable().SingleOrDefaultAsync();

        return entidadDto;
    }


    private IQueryable<ClienteDto> ConsultaDto(IQueryable<Cliente> consulta)
    {

        return consulta.Select(x => new ClienteDto()
        {
            Id = x.Id,
            Nombres = x.Nombres,
            Apellidos = x.Apellidos,
            Identificacion = x.Identificacion,
            Telefonos = x.Telefonos,
            ClienteCategoriaId = x.ClienteCategoriaId,
            ClienteCategoria = x.ClienteCategoria.Nombre
        }
                                );
    }
}
