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
        public virtual List<Artista> Artistas { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public virtual Album Album { get; set; } = null!;
        public Duracao Duracao { get; set; } = null!;
    }
}
