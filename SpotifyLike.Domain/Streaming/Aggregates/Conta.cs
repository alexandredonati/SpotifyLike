using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyLike.Domain.Conta.Aggregates;

namespace SpotifyLike.Domain.Streaming.Aggregates
{
    public class Conta
    {
        public Guid Id { get; set; }

        public Assinatura Assinatura { get; set; }

        public List <Musica> MusicasFavoritas { get; set; }
        public List <Artista> ArtistasFavoritos { get; set; }

    }
}
