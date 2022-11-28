namespace Curso.ComercioElectronico.Domain;

public interface ITipoProductoRepository :  IRepository<TipoProducto, string> {


    Task<bool> ExisteNombre(string nombre, string? idExcluir = null);


}
