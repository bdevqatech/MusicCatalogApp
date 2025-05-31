using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.API.DTOs
{
    public class LoginUserDto
    {
        [Required]
        public string UsernameOrEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
