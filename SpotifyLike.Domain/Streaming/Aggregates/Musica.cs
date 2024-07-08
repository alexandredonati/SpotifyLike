using SpotifyLike.Domain.Streaming.ValueObject;
using SpotifyLike.Domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Streaming.Aggregates
{
    public class Musica : IIdentifier
    {
        public Guid Id { get; set; }
        public virtual IList<Artista> Artistas { get; set; }
        public string Titulo { get; set; }
        public virtual Album Album { get; set; }
        public Duracao Duracao { get; set; }
    }
}
