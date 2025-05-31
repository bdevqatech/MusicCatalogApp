using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.API.Entities;

public partial class Album
{
    public long Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; }

    public string? AlbumArtist { get; set; }

    public long? ArtistId { get; set; }
    public Artist? Artist { get; set; }

    public long? GenreId { get; set; }
    public Genre? Genre { get; set; }

    public long? RecordLabelId { get; set; }
    public RecordLabel? RecordLabel { get; set; }

    public DateTime? ReleaseDate { get; set; }
    public long DurationInSeconds { get; set; }

    [MaxLength(500)]
    public string? CoverImageUrl { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Track> Tracks { get; set; } = new List<Track>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
