using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiAdvance.Core.DAL.Repositories.Abstract;
using WebApiAdvance.DAL.EFCore;
using WebApiAdvance.Entities;

namespace WebApiAdvance.Core.DAL.Repositories.Concrete.EFCore
{
    public abstract class EFBaseRepository<TEntity,TContex>:IRepository<TEntity>
        where TEntity : class, new()
        where TContex : DbContext
    {
        private readonly TContex _context;
        private readonly DbSet<TEntity> _dbSet;
        public EFBaseRepository(TContex context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }


        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, params string[] includes)
        {
            IQueryable<TEntity> query = GetQuery(includes);
            return filter == null
                ? query.ToListAsync()
                : query.Where(filter).ToListAsync();
        }

        public Task<List<TEntity>> GetAllPaginatedAsync(int page, int size, Expression<Func<TEntity, bool>> filter = null, params string[] includes)
        {
            IQueryable<TEntity> query = GetQuery(includes);
            return filter == null
                ? query.Skip((page - 1) * size).Take(size).ToListAsync()
                : query.Where(filter).Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public Task<TEntity> Get(Expression<Func<TEntity, bool>> filter, params string[] includes)
        {
            IQueryable<TEntity> query = GetQuery(includes);
            return query.Where(filter).FirstOrDefaultAsync();
        }

        

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        private IQueryable<TEntity> GetQuery(string[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }
    }
}
