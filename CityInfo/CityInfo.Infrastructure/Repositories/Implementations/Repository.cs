using CityInfo.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CityInfo.Infrastructure.Repositories.Implementations
{
    public class Repository<TEntity> where TEntity : class
    {
        #region [ Fields ]
        protected readonly CityInfoContext _context;
        #endregion

        #region [ Cosntructure ]
        public Repository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #endregion

        #region [ Methods ]
        public async Task<TEntity?> Get(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
         
        public async Task<IEnumerable<TEntity>?> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task Remove(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        #endregion
    }
}
