using SpotifyLike.Domain.Conta.Aggregates;
using SpotifyLike.Domain.Streaming.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Streaming.Dto
{
    public class PlaylistDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public virtual List<MusicDto> Musicas { get; set; } = new List<MusicDto>();
        public bool IsPublica { get; set; }
        public virtual Guid ProprietarioId { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
