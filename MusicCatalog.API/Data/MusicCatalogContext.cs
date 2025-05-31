using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MusicCatalog.API.Entities;

namespace MusicCatalog.API.Data;

public partial class MusicCatalogContext : DbContext
{
    public MusicCatalogContext()
    {
    }

    public MusicCatalogContext(DbContextOptions<MusicCatalogContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<RecordLabel> RecordLabels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Album -> Artist
        modelBuilder.Entity<Album>()
            .HasOne(a => a.Artist)
            .WithMany(ar => ar.Albums)
            .HasForeignKey(a => a.ArtistId)
            .OnDelete(DeleteBehavior.SetNull);

        // Album -> Genre
        modelBuilder.Entity<Album>()
            .HasOne(a => a.Genre)
            .WithMany(g => g.Albums)
            .HasForeignKey(a => a.GenreId)
            .OnDelete(DeleteBehavior.SetNull);

        // Album -> RecordLabel
        modelBuilder.Entity<Album>()
            .HasOne(a => a.RecordLabel)
            .WithMany(r => r.Albums)
            .HasForeignKey(a => a.RecordLabelId)
            .OnDelete(DeleteBehavior.SetNull);

        // Track -> Album
        modelBuilder.Entity<Track>()
            .HasOne(t => t.Album)
            .WithMany(a => a.Tracks)
            .HasForeignKey(t => t.AlbumId)
            .OnDelete(DeleteBehavior.Cascade);

        // Review -> Album
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Album)
            .WithMany(a => a.Reviews)
            .HasForeignKey(r => r.AlbumId)
            .OnDelete(DeleteBehavior.Cascade);

        // Review -> User
        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
