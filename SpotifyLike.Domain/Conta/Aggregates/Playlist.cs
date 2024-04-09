using SpotifyLike.Domain.Streaming.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Conta.Aggregates
{
    public class Playlist
    {
        public Guid Id { get; set; }
        public String Titulo { get; set; }
        public virtual List<Musica> Musicas { get; set; } = new List<Musica>();
        public Boolean IsPublica { get; set; }
        public virtual Usuario Proprietario { get; set; }
        public DateTime DataCriacao { get; set; }

        public void Adicionar(Musica musica) 
        {
            this.Musicas.Add(musica);
        }

        public void Remover(Musica musica)
        {
            if (this.Musicas.Contains(musica))
            {
                this.Musicas.Remove(musica);
            }
        }
    }
}
