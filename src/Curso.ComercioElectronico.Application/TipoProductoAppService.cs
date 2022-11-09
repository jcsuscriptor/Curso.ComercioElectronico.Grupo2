using AutoMapper;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class TipoProductoAppService : ITipoProductoAppService
{
    private readonly ITipoProductoRepository tipoProductoRepository;
    private readonly IMapper mapper;

    public TipoProductoAppService(ITipoProductoRepository tipoProductoRepository,
        IMapper mapper)
    {
        this.tipoProductoRepository = tipoProductoRepository;
        this.mapper = mapper;
    }

    public async Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto tipoProductoDto)
    {
       
        //Mapeo Dto => Entidad. (Manual)
        //var tipoProducto = new TipoProducto();
        //tipoProducto.Nombre = tipoProductoDto.Nombre;
 
        //Automatico
        var tipoProducto = mapper.Map<TipoProducto>(tipoProductoDto);

        //Persistencia objeto
        tipoProducto = await tipoProductoRepository.AddAsync(tipoProducto);
        await tipoProductoRepository.UnitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        //var tipoProductoCreada = new TipoProductoDto();
        //tipoProductoCreada.Nombre = tipoProducto.Nombre;
        //tipoProductoCreada.Id = tipoProducto.Id;

         var tipoProductoCreada = mapper.Map<TipoProductoDto>(tipoProducto);

 
        return tipoProductoCreada;
    }

    public Task<bool> DeleteAsync(int marcaId)
    {
        throw new NotImplementedException();
    }

    public ListaPaginada<TipoProductoDto> GetAll(int limit = 10, int offset = 0)
    {
        throw new NotImplementedException();
    }

    public Task<TipoProductoDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, TipoProductoCrearActualizarDto marca)
    {
        throw new NotImplementedException();
    }
}

