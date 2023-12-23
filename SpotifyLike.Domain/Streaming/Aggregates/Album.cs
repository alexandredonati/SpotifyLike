using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Streaming.Aggregates
{
    public class Album
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<Artista> Artistas { get; set; }
        public List<Musica> Musicas { get; set; } = new List<Musica>();

        public void AdicionarMusica(Musica musica)
        { 
            this.Musicas.Add(musica);
        }
        public void AdicionarMusica(List<Musica> musicas) 
        {
            this.Musicas.AddRange(musicas);
        }

    }
}
