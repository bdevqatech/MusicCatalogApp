using MusicCatalog.API.Entities;

namespace MusicCatalog.API.Repositories
{
    public interface IAlbumRepository: IRepository<Album>
    {
        // Retrieves an album with its related entities (e.g., artist, tracks, reviews)
        Task<Album?> GetAlbumWithDetailsAsync(long id); 
        Task<IEnumerable<Album>> GetAlbumsByArtistIdAsync(long artistId);
        Task<IEnumerable<Album>> GetAlbumsByGenreIdAsync(int genreId);
        Task<IEnumerable<Album>> GetAlbumsByRecordLabelIdAsync(long recordLabelId);

        // Retrieves all albums with their related entities
        Task<IEnumerable<Album>> GetAllAlbumsWithDetailsAsync(); 

        // Search for albums by title or artist name with pagination support
        Task<IEnumerable<Album>> SearchAlbumsAsync(string searchTerm, int pageNumber, int pageSize);
    }
}
