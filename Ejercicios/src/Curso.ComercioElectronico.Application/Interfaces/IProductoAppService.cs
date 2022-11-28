namespace Curso.ComercioElectronico.Application;



public interface IProductoAppService
{
    Task<ProductoDto> GetByIdAsync(int id);
     
    Task<ListaPaginada<ProductoDto>> GetListAsync(ProductoListInput input);


    Task<ProductoDto> CreateAsync(ProductoCrearActualizarDto producto);

    Task UpdateAsync (int id, ProductoCrearActualizarDto producto);

    Task<bool> DeleteAsync(int marcaId);
}
