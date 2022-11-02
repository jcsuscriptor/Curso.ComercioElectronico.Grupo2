using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class OrdenAppService : IOrdenAppService
{
    private readonly IOrdenRepository ordenRepository; 

    public OrdenAppService(IOrdenRepository ordenRepository)
    {
        this.ordenRepository = ordenRepository;
    }

    public async Task<OrdenDto> CreateAsync(OrdenCrearActualizarDto productoDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnularAsync(int odenId)
    {
        throw new NotImplementedException();
    }

    public ListaPaginada<OrdenDto> GetAll(int limit = 10, int offset = 0)
    {
         throw new NotImplementedException();

    }

    public Task<OrdenDto> GetByIdAsync(int id)
    {
         throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, OrdenCrearActualizarDto marca)
    {
        throw new NotImplementedException();
    }
}

