namespace Curso.ComercioElectronico.Domain;

public interface IProductoRepository :  IRepository<Producto> {

   Task<ICollection<Producto>> GetListAsync(IList<int> listaIds, bool asNoTracking = true);

}
