namespace MusicCatalog.API.DTOs
{
    public class TrackDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int TrackNumber { get; set; }
        public long DurationInSeconds { get; set; }
    }
}
