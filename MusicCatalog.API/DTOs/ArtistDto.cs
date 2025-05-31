namespace MusicCatalog.API.DTOs
{
    public class ArtistDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? Country { get; set; }
        public string? Website { get; set; }
        public string? ImageUrl { get; set; }
    }
}
