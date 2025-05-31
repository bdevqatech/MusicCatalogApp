using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.API.DTOs
{
    public class CreateArtistDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? Country { get; set; }
        public string? Website { get; set; }
        public string? ImageUrl { get; set; }
    }
}
