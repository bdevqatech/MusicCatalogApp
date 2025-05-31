using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.API.DTOs
{
    public class CreateReviewDto
    {
        [Required]
        public long AlbumId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }

        public string? UserId { get; set; } // Optional, can be set by the API if not provided
    }
}
