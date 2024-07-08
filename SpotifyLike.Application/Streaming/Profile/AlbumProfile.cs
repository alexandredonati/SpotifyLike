using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;

namespace SpotifyLike.Application.Streaming.Profile
{
    public class AlbumProfile : AutoMapper.Profile
    {
        public AlbumProfile() 
        {
            CreateMap<Album, AlbumDto>()
                .AfterMap((s, d) =>
                {
                    d.ArtistIds = s.Artistas.Select(a => a.Id).ToList();
                }).ReverseMap();
        }
    }
}
