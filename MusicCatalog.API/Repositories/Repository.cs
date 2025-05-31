
using Microsoft.EntityFrameworkCore;
using MusicCatalog.API.Data;

namespace MusicCatalog.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MusicCatalogContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(MusicCatalogContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(object id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Remove(T entity) => _dbSet.Remove(entity);
        public IQueryable<T> Query() => _dbSet.AsQueryable();
    }
}
