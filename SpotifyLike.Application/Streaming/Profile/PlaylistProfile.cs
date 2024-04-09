using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Streaming.Profile
{
    public class PlaylistProfile : AutoMapper.Profile
    {
        public PlaylistProfile()
        {
            CreateMap<Playlist, PlaylistDto>()
                .AfterMap((s, d) =>
                {
                    d.Musicas = s.Musicas.Select(m => new MusicDto
                    {
                        Id = m.Id,
                        Titulo = m.Titulo,
                        Duracao = m.Duracao,
                    }).ToList();

                    d.ProprietarioId = s.Proprietario.Id;
                })
                .ReverseMap();
        }   
    }
}
