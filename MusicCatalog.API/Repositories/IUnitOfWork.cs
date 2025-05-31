using MusicCatalog.API.Entities;

namespace MusicCatalog.API.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Artist> Artists { get; }
        IRepository<Album> Albums { get; }
        IRepository<Track> Tracks { get; }
        IRepository<Genre> Genres { get; }
        IRepository<Review> Reviews { get; }
        IRepository<RecordLabel> RecordLabels { get; }
        IRepository<User> Users { get; }
        Task<int> SaveChangesAsync();
    }
}
