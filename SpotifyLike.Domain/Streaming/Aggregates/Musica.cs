using SpotifyLike.Domain.Streaming.ValueObject;
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
        public List<Artista> Artistas { get; set; }
        public String Titulo { get; set; }
        public Album Album { get; set; }
        public Duracao Duracao { get; set; }    

    }
}
