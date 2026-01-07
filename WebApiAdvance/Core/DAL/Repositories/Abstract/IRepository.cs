using System.Linq.Expressions;
using WebApiAdvance.Entities;

namespace WebApiAdvance.Core.DAL.Repositories.Abstract
{
    public interface IRepository<TEntity>
        where TEntity : class, new()
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, params string[] includes);
        Task<List<TEntity>> GetAllPaginatedAsync(int page, int size, Expression<Func<TEntity, bool>> filter = null, params string[] includes);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> filter, params string[] includes);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(Guid id);
    }
}
