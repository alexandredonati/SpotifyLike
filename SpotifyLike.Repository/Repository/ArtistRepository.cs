using Microsoft.EntityFrameworkCore;
using SpotifyLike.Domain.Streaming.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Repository.Repository
{
    public class ArtistRepository : RepositoryBase<Artista>
    {
        public SpotifyLikeContext Context { get; set; }
        public ArtistRepository(SpotifyLikeContext context) : base(context)
        {
            Context = context;
        }
        public new Artista? GetById(Guid id)
        {
            var dataArtist = this.Context.Artistas
                       .Include(x => x.Albums)
                       .ThenInclude(x => x.Musicas)
                       .FirstOrDefault(x => x.Id == id);

            if(dataArtist != null)
            {
                return new Artista
                {
                    Id = dataArtist.Id,
                    Nome = dataArtist.Nome,
                    Descricao = dataArtist.Descricao,
                    Backdrop = dataArtist.Backdrop,
                    Albums = dataArtist.Albums,
                    Musicas = dataArtist.Albums.SelectMany(album => album.Musicas).ToList()
                };
            }
            return null;
        }
    }
}
