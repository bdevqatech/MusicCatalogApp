using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.API.Entities;

public partial class RecordLabel
{
    public long Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; }

    public string? Website { get; set; }

    [MaxLength(50)]
    public string? Country { get; set; }

    public ICollection<Album> Albums { get; set; } = new List<Album>();
}
