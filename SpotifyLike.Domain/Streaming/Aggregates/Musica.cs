using SpotifyLike.Domain.Streaming.ValueObject;
using SpotifyLike.Domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Streaming.Aggregates
{
    public class Musica
    {
        public Guid Id { get; set; }
        public List<Artista> Artistas { get; set; } = new List<Artista>();
        public String Titulo { get; set; }
        public Album Album { get; set; }
        public Duracao Duracao { get; set; }

        public List<Playlist> Playlists { get; set; }
    }
}
