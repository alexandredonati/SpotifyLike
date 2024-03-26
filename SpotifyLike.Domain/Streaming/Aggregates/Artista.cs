using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Domain.Streaming.Aggregates
{
    public class Artista
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string? Descricao { get; set; }
        public string? Backdrop { get; set; }
        public virtual IList<Musica> Musicas { get; set; } = new List<Musica>();
        public virtual IList<Album> Albums { get; set; } = new List<Album>();

        public void AdicionarAlbum(Album album) 
        { 
            this.Albums.Add(album); 
        }
    }
}
