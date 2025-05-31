using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.API.Entities;

public partial class Artist
{
    public long Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; }

    public string? Bio { get; set; }
    public string? Country { get; set; }
    public string? Website { get; set; }
    public string? ImageUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Album> Albums { get; set; } = new List<Album>();
}
