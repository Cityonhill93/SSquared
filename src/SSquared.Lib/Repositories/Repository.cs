using Microsoft.EntityFrameworkCore;
using SSquared.Lib.Data;
using SSquared.Lib.Data.Entities;

namespace SSquared.Lib.Repositories
{
    public abstract class Repository<T> : IRepository<T>
        where T : class, IEntity
    {
        public Repository(SSquaredDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Ideally, we could have some kind of abstraction over the DB context, so that we could easily test the logic/filters in our get methods, but due to the short time before the interview, this will have to suffice
        protected readonly SSquaredDbContext _dbContext;

        public virtual async Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<T?> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        protected virtual IQueryable<T> GetQueryable() => _dbContext.Set<T>();
    }
}
