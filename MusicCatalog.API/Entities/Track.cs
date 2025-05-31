using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.API.Entities;

public partial class Track
{
    public long Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; }

    public int TrackNumber { get; set; }
    public long DurationInSeconds { get; set; }

    public long AlbumId { get; set; }
    public Album Album { get; set; }
}
