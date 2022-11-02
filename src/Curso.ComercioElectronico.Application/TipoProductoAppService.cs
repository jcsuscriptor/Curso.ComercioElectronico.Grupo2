using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class TipoProductoAppService : ITipoProductoAppService
{
    private readonly ITipoProductoRepository tipoProductoRepository;

    public TipoProductoAppService(ITipoProductoRepository tipoProductoRepository)
    {
        this.tipoProductoRepository = tipoProductoRepository;
    }

    public async Task<TipoProductoDto> CreateAsync(TipoProductoCrearActualizarDto tipoProductoDto)
    {
       
        //Mapeo Dto => Entidad
        var tipoProducto = new TipoProducto();
        tipoProducto.Nombre = tipoProductoDto.Nombre;
 
        //Persistencia objeto
        tipoProducto = await tipoProductoRepository.AddAsync(tipoProducto);
        await tipoProductoRepository.UnitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        var tipoProductoCreada = new TipoProductoDto();
        tipoProductoCreada.Nombre = tipoProducto.Nombre;
        tipoProductoCreada.Id = tipoProducto.Id;

 
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

