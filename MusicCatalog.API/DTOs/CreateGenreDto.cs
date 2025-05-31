using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.API.DTOs
{
    public class CreateGenreDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
