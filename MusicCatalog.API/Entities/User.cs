using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.API.Entities
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } 

        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public string? Role { get; set; } = "User";

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
