namespace MusicCatalog.API.DTOs
{
    public class AlbumDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string? AlbumArtist { get; set; }
        public string? Genre { get; set; }
        public string? RecordLabel { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public long DurationInSeconds { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? Description { get; set; }
        public double? AverageRating { get; set; }
        public int ReviewCount { get; set; }
    }
}
