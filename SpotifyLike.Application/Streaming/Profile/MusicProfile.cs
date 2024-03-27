using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;

namespace SpotifyLike.Application.Streaming.Profile
{
    public class MusicProfile : AutoMapper.Profile
    {
        public MusicProfile() 
        {
            CreateMap<MusicDto, Musica>()
                .ReverseMap();
        }
    }
}
