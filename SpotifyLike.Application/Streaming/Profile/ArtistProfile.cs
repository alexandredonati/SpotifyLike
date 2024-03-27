using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;

namespace SpotifyLike.Application.Streaming.Profile
{
    public class ArtistProfile : AutoMapper.Profile
    {
        public ArtistProfile()
        {
            CreateMap<ArtistDto, Artista>()
                .ReverseMap();
        }
    }
}
