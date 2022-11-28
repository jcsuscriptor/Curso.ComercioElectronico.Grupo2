namespace Curso.ComercioElectronico.Application;

public interface ITipoProductoAppService
{
    Task<TipoProductoDto> GetByIdAsync(string id);

   
    Task<ICollection<TipoProductoDto>> GetAllAsync();

    Task<TipoProductoDto> CreateAsync(TipoProductoCrearDto marca);

    Task UpdateAsync (string id, TipoProductoActualizarDto marca);

    Task<bool> DeleteAsync(string marcaId);
}

