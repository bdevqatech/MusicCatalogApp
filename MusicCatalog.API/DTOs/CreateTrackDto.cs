using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.API.DTOs
{
    public class CreateTrackDto
    {
        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Range(1, long.MaxValue)]
        public long DurationInSeconds { get; set; }

        [Required]
        public long AlbumId { get; set; }

        public int TrackNumber { get; set; } = 1; // Default to 1 if not specified
    }
}
