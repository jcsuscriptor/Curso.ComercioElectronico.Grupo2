namespace Curso.ComercioElectronico.Application;



public interface IOrdenAppService
{
    Task<OrdenDto> GetByIdAsync(int id);

    ListaPaginada<OrdenDto> GetAll(int limit=10,int offset=0);

    //ListaPaginada<OrdenDto> GetByClientIdAll(int clientId, int limit=10,int offset=0);


    Task<OrdenDto> CreateAsync(OrdenCrearActualizarDto orden);

    Task UpdateAsync (int id, OrdenCrearActualizarDto orden);

    Task<bool> AnularAsync(int ordenId);
}

