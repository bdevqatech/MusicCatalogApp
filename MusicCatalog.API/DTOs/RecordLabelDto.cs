namespace MusicCatalog.API.DTOs
{
    public class RecordLabelDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Website { get; set; }
        public string? Country { get; set; }
    }
}
