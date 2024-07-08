using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Streaming.Aggregates;

namespace SpotifyLike.Application.Streaming.Profile
{
    public class MusicProfile : AutoMapper.Profile
    {
        public MusicProfile() 
        {
            CreateMap<Musica, SongDto>()
                .AfterMap((s, d) =>
                {
                    d.AlbumId = s.Album.Id;
                    d.ArtistsIds = s.Artistas.Select(a => a.Id).ToList();
                    d.Duracao = s.Duracao.Valor;
                })
                .ReverseMap();
        }
    }
}
