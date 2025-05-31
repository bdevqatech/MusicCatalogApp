using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.API.DTOs
{
    public class CreateRecordLabelDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string? LabelCode { get; set; }

        public string? Website { get; set; }

        [MaxLength(50)]
        public string? Country { get; set; }
    }
}
