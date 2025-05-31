namespace MusicCatalog.API.DTOs
{
    public class ReviewDto
    {
        public long Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
    }
}
