using System.Linq.Expressions;

namespace Curso.ComercioElectronico.Domain;

public interface IOrdenRepository :  IRepository<Orden,Guid> {

    IQueryable<Orden> GetDetails();

}
