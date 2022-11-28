
using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Curso.ComercioElectronico.Infraestructure;

public class OrdenRepository : EfRepository<Orden,Guid>, IOrdenRepository
{
    public OrdenRepository(ComercioElectronicoDbContext context) : base(context)
    {
    }

    public IQueryable<Orden> GetDetails()
    {
        var queryable = GetQueryable();
        queryable = queryable.Include(x => x.Cliente);
        queryable = queryable.Include(x => x.Items)
                    .ThenInclude(i => i.Product);

        return queryable;
    }
}