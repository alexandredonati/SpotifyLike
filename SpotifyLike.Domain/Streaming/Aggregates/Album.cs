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
        public string Nome { get; set; } = null!;
        public virtual IList<Artista> Artistas { get; set; } = new List<Artista>();
        public virtual IList<Musica> Musicas { get; set; } = new List<Musica>();

        public void AdicionarMusica(Musica musica)
        { 
            this.Musicas.Add(musica);
        }
        public void AdicionarMusicas(IList<Musica> musicas)
        {
            if (this.Musicas is List<Musica> listMusicas) 
            {
                listMusicas.AddRange(musicas);
            }
            else
            {
                foreach (var musica in musicas)
                {
                    this.Musicas.Add(musica);
                }
            }
        }

    }
}
