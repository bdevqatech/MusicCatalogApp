using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.API.DTOs
{
    public class CreateAlbumDto
    {
        [Required, MaxLength(200)]
        public string Title { get; set; }

        public string? AlbumArtist { get; set; }
        public long? GenreId { get; set; }
        public long? RecordLabelId { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
