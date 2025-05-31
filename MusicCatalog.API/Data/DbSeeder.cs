using Microsoft.EntityFrameworkCore;
using MusicCatalog.API.Entities;
using System.Security.Cryptography;
using System.Text;

namespace MusicCatalog.API.Data
{
    public static class DbSeeder
    {
        public static async Task SeedDatabase(MusicCatalogContext context)
        {
            // Only seed if database is empty
            if (await context.Albums.AnyAsync() || await context.Artists.AnyAsync())
                return;

            // Seed Genres
            await SeedGenres(context);
            
            // Seed Record Labels
            await SeedRecordLabels(context);
            
            // Seed Artists
            await SeedArtists(context);
            
            // Seed Albums (and Tracks)
            await SeedAlbums(context);
            
            // Seed Users
            await SeedUsers(context);
            
            // Seed Reviews (requires Users and Albums)
            await SeedReviews(context);
        }

        private static async Task SeedGenres(MusicCatalogContext context)
        {
            var genres = new List<Genre>
            {
                new Genre { Name = "Rock", Description = "Rock music is a broad genre of popular music that originated as rock and roll in the United States in the late 1940s and early 1950s." },
                new Genre { Name = "Pop", Description = "Pop music is a genre of popular music that originated in its modern form during the mid-1950s in the United States and the United Kingdom." },
                new Genre { Name = "Hip Hop", Description = "Hip hop music, also known as rap music, is a genre of popular music that originated in New York City in the 1970s." },
                new Genre { Name = "Electronic", Description = "Electronic music is music that employs electronic musical instruments, digital instruments, or circuitry-based music technology in its creation." },
                new Genre { Name = "Jazz", Description = "Jazz is a music genre that originated in the African-American communities of New Orleans, Louisiana, in the late 19th and early 20th centuries." },
                new Genre { Name = "Classical", Description = "Classical music is art music produced or rooted in the traditions of Western culture, including both liturgical and secular music." },
                new Genre { Name = "R&B", Description = "Rhythm and blues, often abbreviated as R&B, is a genre of popular music that originated in African-American communities in the 1940s." },
                new Genre { Name = "Country", Description = "Country music, also known as country and western, is a genre of popular music that originated in the Southern United States in the early 1920s." }
            };

            await context.Genres.AddRangeAsync(genres);
            await context.SaveChangesAsync();
        }

        private static async Task SeedRecordLabels(MusicCatalogContext context)
        {
            var recordLabels = new List<RecordLabel>
            {
                new RecordLabel { Name = "Columbia Records", Website = "https://www.columbiarecords.com", Country = "United States" },
                new RecordLabel { Name = "Atlantic Records", Website = "https://www.atlanticrecords.com", Country = "United States" },
                new RecordLabel { Name = "Interscope Records", Website = "https://www.interscope.com", Country = "United States" },
                new RecordLabel { Name = "Warner Records", Website = "https://www.warnerrecords.com", Country = "United States" },
                new RecordLabel { Name = "Island Records", Website = "https://www.islandrecords.com", Country = "United Kingdom" },
                new RecordLabel { Name = "Capitol Records", Website = "https://www.capitolrecords.com", Country = "United States" }
            };

            await context.RecordLabels.AddRangeAsync(recordLabels);
            await context.SaveChangesAsync();
        }

        private static async Task SeedArtists(MusicCatalogContext context)
        {
            var artists = new List<Artist>
            {
                new Artist { 
                    Name = "The Beatles", 
                    Bio = "The Beatles were an English rock band formed in Liverpool in 1960.", 
                    Country = "United Kingdom", 
                    Website = "https://www.thebeatles.com", 
                    ImageUrl = "https://example.com/beatles.jpg" 
                },
                new Artist { 
                    Name = "Michael Jackson", 
                    Bio = "Michael Joseph Jackson was an American singer, songwriter, and dancer.", 
                    Country = "United States", 
                    Website = "https://www.michaeljackson.com", 
                    ImageUrl = "https://example.com/mj.jpg" 
                },
                new Artist { 
                    Name = "Queen", 
                    Bio = "Queen are a British rock band formed in London in 1970.", 
                    Country = "United Kingdom", 
                    Website = "https://www.queenonline.com", 
                    ImageUrl = "https://example.com/queen.jpg" 
                },
                new Artist { 
                    Name = "Taylor Swift", 
                    Bio = "Taylor Alison Swift is an American singer-songwriter.", 
                    Country = "United States", 
                    Website = "https://www.taylorswift.com", 
                    ImageUrl = "https://example.com/taylor.jpg" 
                },
                new Artist { 
                    Name = "Kendrick Lamar", 
                    Bio = "Kendrick Lamar Duckworth is an American rapper, songwriter, and record producer.", 
                    Country = "United States", 
                    Website = "https://www.kendricklamar.com", 
                    ImageUrl = "https://example.com/kendrick.jpg" 
                }
            };

            await context.Artists.AddRangeAsync(artists);
            await context.SaveChangesAsync();
        }

        private static async Task SeedAlbums(MusicCatalogContext context)
        {
            var rockGenre = await context.Genres.FirstOrDefaultAsync(g => g.Name == "Rock");
            var popGenre = await context.Genres.FirstOrDefaultAsync(g => g.Name == "Pop");
            var hipHopGenre = await context.Genres.FirstOrDefaultAsync(g => g.Name == "Hip Hop");
            
            var columbiaRecords = await context.RecordLabels.FirstOrDefaultAsync(r => r.Name == "Columbia Records");
            var atlanticRecords = await context.RecordLabels.FirstOrDefaultAsync(r => r.Name == "Atlantic Records");
            var interscope = await context.RecordLabels.FirstOrDefaultAsync(r => r.Name == "Interscope Records");
            
            var beatles = await context.Artists.FirstOrDefaultAsync(a => a.Name == "The Beatles");
            var michaelJackson = await context.Artists.FirstOrDefaultAsync(a => a.Name == "Michael Jackson");
            var queen = await context.Artists.FirstOrDefaultAsync(a => a.Name == "Queen");
            var taylorSwift = await context.Artists.FirstOrDefaultAsync(a => a.Name == "Taylor Swift");
            var kendrickLamar = await context.Artists.FirstOrDefaultAsync(a => a.Name == "Kendrick Lamar");

            var albums = new List<Album>();

            // Beatles Album
            var abbeyRoad = new Album
            {
                Title = "Abbey Road",
                AlbumArtist = "The Beatles",
                Artist = beatles,
                Genre = rockGenre,
                RecordLabel = atlanticRecords,
                ReleaseDate = new DateTime(1969, 9, 26),
                DurationInSeconds = 2532, // 42:12
                CoverImageUrl = "https://example.com/abbey_road.jpg",
                Description = "Abbey Road is the eleventh studio album by the English rock band the Beatles, released on 26 September 1969.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Tracks = new List<Track>
                {
                    new Track { Title = "Come Together", TrackNumber = 1, DurationInSeconds = 259 },
                    new Track { Title = "Something", TrackNumber = 2, DurationInSeconds = 183 },
                    new Track { Title = "Maxwell's Silver Hammer", TrackNumber = 3, DurationInSeconds = 207 },
                    new Track { Title = "Oh! Darling", TrackNumber = 4, DurationInSeconds = 207 },
                    new Track { Title = "Octopus's Garden", TrackNumber = 5, DurationInSeconds = 171 }
                }
            };
            albums.Add(abbeyRoad);

            // Michael Jackson Album
            var thriller = new Album
            {
                Title = "Thriller",
                AlbumArtist = "Michael Jackson",
                Artist = michaelJackson,
                Genre = popGenre,
                RecordLabel = columbiaRecords,
                ReleaseDate = new DateTime(1982, 11, 30),
                DurationInSeconds = 2408, // 40:08
                CoverImageUrl = "https://example.com/thriller.jpg",
                Description = "Thriller is the sixth studio album by American singer Michael Jackson, released on November 30, 1982.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Tracks = new List<Track>
                {
                    new Track { Title = "Wanna Be Startin' Somethin'", TrackNumber = 1, DurationInSeconds = 363 },
                    new Track { Title = "Baby Be Mine", TrackNumber = 2, DurationInSeconds = 260 },
                    new Track { Title = "The Girl Is Mine", TrackNumber = 3, DurationInSeconds = 222 },
                    new Track { Title = "Thriller", TrackNumber = 4, DurationInSeconds = 357 },
                    new Track { Title = "Beat It", TrackNumber = 5, DurationInSeconds = 258 }
                }
            };
            albums.Add(thriller);

            // Queen Album
            var aKindOfMagic = new Album
            {
                Title = "A Kind of Magic",
                AlbumArtist = "Queen",
                Artist = queen,
                Genre = rockGenre,
                RecordLabel = atlanticRecords,
                ReleaseDate = new DateTime(1986, 6, 2),
                DurationInSeconds = 2315, // 38:35
                CoverImageUrl = "https://example.com/a_kind_of_magic.jpg",
                Description = "A Kind of Magic is the twelfth studio album by the British rock band Queen, released on 2 June 1986.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Tracks = new List<Track>
                {
                    new Track { Title = "One Vision", TrackNumber = 1, DurationInSeconds = 310 },
                    new Track { Title = "A Kind of Magic", TrackNumber = 2, DurationInSeconds = 263 },
                    new Track { Title = "One Year of Love", TrackNumber = 3, DurationInSeconds = 266 },
                    new Track { Title = "Pain Is So Close to Pleasure", TrackNumber = 4, DurationInSeconds = 254 },
                    new Track { Title = "Friends Will Be Friends", TrackNumber = 5, DurationInSeconds = 248 }
                }
            };
            albums.Add(aKindOfMagic);

            // Kendrick Lamar Album
            var topimp = new Album
            {
                Title = "To Pimp a Butterfly",
                AlbumArtist = "Kendrick Lamar",
                Artist = kendrickLamar,
                Genre = hipHopGenre,
                RecordLabel = interscope,
                ReleaseDate = new DateTime(2015, 3, 15),
                DurationInSeconds = 4888, // 1:21:28
                CoverImageUrl = "https://example.com/tpab.jpg",
                Description = "To Pimp a Butterfly is the third studio album by American rapper Kendrick Lamar, released on March 15, 2015.",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Tracks = new List<Track>
                {
                    new Track { Title = "Wesley's Theory", TrackNumber = 1, DurationInSeconds = 287 },
                    new Track { Title = "For Free? (Interlude)", TrackNumber = 2, DurationInSeconds = 130 },
                    new Track { Title = "King Kunta", TrackNumber = 3, DurationInSeconds = 234 },
                    new Track { Title = "Institutionalized", TrackNumber = 4, DurationInSeconds = 274 },
                    new Track { Title = "These Walls", TrackNumber = 5, DurationInSeconds = 300 }
                }
            };
            albums.Add(topimp);

            await context.Albums.AddRangeAsync(albums);
            await context.SaveChangesAsync();
        }

        private static async Task SeedUsers(MusicCatalogContext context)
        {
            // Create a test user
            using var hmac = new HMACSHA512();
            
            var testUser = new User
            {
                Username = "testuser",
                Email = "test@example.com",
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Password123!")),
                PasswordSalt = hmac.Key,
                Role = "User"
            };

            var adminUser = new User
            {
                Username = "admin",
                Email = "admin@example.com",
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Admin123!")),
                PasswordSalt = hmac.Key,
                Role = "Admin"
            };

            await context.Users.AddRangeAsync(new List<User> { testUser, adminUser });
            await context.SaveChangesAsync();
        }

        private static async Task SeedReviews(MusicCatalogContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == "testuser");
            var admin = await context.Users.FirstOrDefaultAsync(u => u.Username == "admin");
            
            if (user == null || admin == null)
                return;

            var albums = await context.Albums.ToListAsync();
            var reviews = new List<Review>();

            foreach (var album in albums)
            {
                // Add user review
                reviews.Add(new Review
                {
                    AlbumId = album.Id,
                    UserId = user.Id,
                    Rating = Random.Shared.Next(3, 6), // 3-5 stars
                    Comment = $"Great album! I love {album.Title} by {album.AlbumArtist}.",
                    CreatedAt = DateTime.UtcNow.AddDays(-Random.Shared.Next(1, 30)),
                    UpdatedAt = DateTime.UtcNow
                });

                // Add admin review
                reviews.Add(new Review
                {
                    AlbumId = album.Id,
                    UserId = admin.Id,
                    Rating = Random.Shared.Next(4, 6), // 4-5 stars
                    Comment = $"As a music critic, I find {album.Title} to be a masterpiece of {album.Genre?.Name} music.",
                    CreatedAt = DateTime.UtcNow.AddDays(-Random.Shared.Next(1, 30)),
                    UpdatedAt = DateTime.UtcNow
                });
            }

            await context.Reviews.AddRangeAsync(reviews);
            await context.SaveChangesAsync();
        }
    }
}