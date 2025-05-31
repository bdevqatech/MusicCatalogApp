using Microsoft.EntityFrameworkCore;
using MusicCatalog.API.Data;
using MusicCatalog.API.Entities;

namespace MusicCatalog.API.Repositories
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        public AlbumRepository(MusicCatalogContext context) : base(context)
        {
        }

        public async Task<Album?> GetAlbumWithDetailsAsync(long id)
        {
            return await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Include(a => a.RecordLabel)
                .Include(a => a.Tracks)
                .Include(a => a.Reviews)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Album>> GetAllAlbumsWithDetailsAsync()
        {
            return await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Include(a => a.RecordLabel)
                .Include(a => a.Tracks)
                .Include(a => a.Reviews)
                    .ThenInclude(r => r.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Album>> GetAlbumsByArtistIdAsync(long artistId)
        {
            return await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Include(a => a.RecordLabel)
                .Include(a => a.Tracks)
                .Include(a => a.Reviews)
                    .ThenInclude(r => r.User)
                .Where(a => a.ArtistId == artistId)
                .OrderByDescending(a => a.ReleaseDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Album>> GetAlbumsByGenreIdAsync(int genreId)
        {
            return await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Include(a => a.RecordLabel)
                .Include(a => a.Tracks)
                .Include(a => a.Reviews)
                    .ThenInclude(r => r.User)
                .Where(a => a.GenreId == genreId)
                .OrderByDescending(a => a.ReleaseDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Album>> GetAlbumsByRecordLabelIdAsync(long recordLabelId)
        {
            return await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Include(a => a.RecordLabel)
                .Include(a => a.Tracks)
                .Include(a => a.Reviews)
                    .ThenInclude(r => r.User)
                .Where(a => a.RecordLabelId == recordLabelId)
                .OrderByDescending(a => a.ReleaseDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Album>> SearchAlbumsAsync(string searchTerm, int pageNumber, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAlbumsWithDetailsAsync();
            }

            searchTerm = searchTerm.ToLower().Trim();

            return await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Include(a => a.RecordLabel)
                .Include(a => a.Tracks)
                .Include(a => a.Reviews)
                    .ThenInclude(r => r.User)
                .Where(a => a.Title.ToLower().Contains(searchTerm) ||
                            (a.Artist != null && a.Artist.Name.ToLower().Contains(searchTerm)) ||
                            (a.AlbumArtist != null && a.AlbumArtist.ToLower().Contains(searchTerm)))
                .OrderByDescending(a => a.ReleaseDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
