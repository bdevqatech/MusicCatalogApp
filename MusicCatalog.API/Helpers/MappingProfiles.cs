namespace MusicCatalog.API.Helpers
{
    using AutoMapper;
    using MusicCatalog.API.DTOs;
    using MusicCatalog.API.Entities;

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Album
            CreateMap<Album, AlbumDto>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre != null ? src.Genre.Name : null))
                .ForMember(dest => dest.RecordLabel, opt => opt.MapFrom(src => src.RecordLabel != null ? src.RecordLabel.Name : null))
                .ForMember(dest => dest.AlbumArtist, opt => opt.MapFrom(src => src.AlbumArtist))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src =>
                    src.Reviews != null && src.Reviews.Any()
                    ? Math.Round(src.Reviews.Average(r => r.Rating), 1)
                    : (double?)null))
                .ForMember(dest => dest.ReviewCount, opt => opt.MapFrom(src =>
                    src.Reviews != null ? src.Reviews.Count : 0));

            CreateMap<CreateAlbumDto, Album>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.AlbumArtist, opt => opt.MapFrom(src => src.AlbumArtist))
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
                .ForMember(dest => dest.RecordLabelId, opt => opt.MapFrom(src => src.RecordLabelId))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ArtistId, opt => opt.Ignore())
                .ForMember(dest => dest.Artist, opt => opt.Ignore())
                .ForMember(dest => dest.Genre, opt => opt.Ignore())
                .ForMember(dest => dest.RecordLabel, opt => opt.Ignore())
                .ForMember(dest => dest.DurationInSeconds, opt => opt.Ignore())
                .ForMember(dest => dest.CoverImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Tracks, opt => opt.Ignore())
                .ForMember(dest => dest.Reviews, opt => opt.Ignore());

            // Artist
            CreateMap<Artist, ArtistDto>();
            CreateMap<CreateArtistDto, Artist>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Albums, opt => opt.Ignore());


            // Track
            CreateMap<Track, TrackDto>();
            CreateMap<CreateTrackDto, Track>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.TrackNumber, opt => opt.MapFrom(src => src.TrackNumber))
                .ForMember(dest => dest.DurationInSeconds, opt => opt.MapFrom(src => src.DurationInSeconds))
                .ForMember(dest => dest.AlbumId, opt => opt.MapFrom(src => src.AlbumId))
                .ForMember(dest => dest.Album, opt => opt.Ignore());

            // User
            CreateMap<User, UserDto>();
            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User"));

            // Review
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));
            CreateMap<CreateReviewDto, Review>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(dest => dest.AlbumId, opt => opt.MapFrom(src => src.AlbumId))
                .ForMember(dest => dest.Album, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());


            // Genre
            CreateMap<Genre, GenreDto>();
            CreateMap<CreateGenreDto, Genre>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Albums, opt => opt.Ignore());

            // RecordLabel
            CreateMap<RecordLabel, RecordLabelDto>();
            CreateMap<CreateRecordLabelDto, RecordLabel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
                .ForMember(dest => dest.Albums, opt => opt.Ignore());


        }
    }
}
