using MusicCatalog.API.Data;
using MusicCatalog.API.Entities;

namespace MusicCatalog.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MusicCatalogContext _context;
        public IRepository<Artist> Artists { get; }
        public IRepository<Album> Albums { get; }
        public IRepository<Track> Tracks { get; }
        public IRepository<Review> Reviews { get; }
        public IRepository<Genre> Genres { get; }
        public IRepository<RecordLabel> RecordLabels { get; }
        public IRepository<User> Users { get; }

        public UnitOfWork(MusicCatalogContext context)
        {
            _context = context;
            Users = new Repository<User>(_context);
            Artists = new Repository<Artist>(_context);
            Albums = new Repository<Album>(_context);
            Tracks = new Repository<Track>(_context);
            Reviews = new Repository<Review>(_context);
            Genres = new Repository<Genre>(_context);
            RecordLabels = new Repository<RecordLabel>(_context);
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
