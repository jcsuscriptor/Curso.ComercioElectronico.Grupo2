
using System.Linq.Expressions;

namespace Curso.ComercioElectronico.Domain;




public interface IRepository<TEntity,TEntityId> where TEntity : class
{
    IUnitOfWork UnitOfWork { get; }
    
    IQueryable<TEntity> GetQueryable(bool asNoTracking = true);

    Task<ICollection<TEntity>> GetAllAsync(bool asNoTracking = true);

    Task<TEntity> GetByIdAsync(TEntityId id);


    Task<TEntity> AddAsync(TEntity entity);


    Task UpdateAsync (TEntity entity);

    Task DeleteAsync(TEntity entity);

    IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
}
