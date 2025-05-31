using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.API.DTOs
{
    public class RegisterUserDto
    {
        [Required, MaxLength(100)]
        public string Username { get; set; }

        [Required, EmailAddress, MaxLength(256)]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
