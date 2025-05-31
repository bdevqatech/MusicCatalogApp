using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.API.Entities;

public partial class Genre
{
    public long Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    public string? Description { get; set; }

    public ICollection<Album> Albums { get; set; } = new List<Album>();
}
