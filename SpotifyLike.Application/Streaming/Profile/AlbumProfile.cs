using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;

namespace SpotifyLike.Application.Streaming.Profile
{
    public class AlbumProfile : AutoMapper.Profile
    {
        public AlbumProfile() 
        {
            CreateMap<AlbumDto, Album>()
                .ReverseMap();
        }
    }
}
