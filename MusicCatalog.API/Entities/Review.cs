using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.API.Entities;

public partial class Review
{
    public long Id { get; set; }

    public long AlbumId { get; set; }
    public Album Album { get; set; }

    [Required]
    public string UserId { get; set; }
    public User User { get; set; }


    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(1000)]
    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}
