namespace Curso.ComercioElectronico.Application;



public interface IOrdenAppService
{
    Task<OrdenDto> GetByIdAsync(Guid id);

    
    Task<ListaPaginada<OrdenDto>> GetListAsync(OrdenListInput input);
 
    Task<OrdenDto> CreateAsync(OrdenCrearDto orden);

    Task UpdateAsync (Guid id, OrdenActualizarDto orden);

    Task<bool> AnularAsync(Guid ordenId);
}

