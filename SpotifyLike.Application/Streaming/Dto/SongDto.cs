using SpotifyLike.Domain.Streaming.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Streaming.Dto
{
    public class SongDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public int Duracao { get; set; }

        public Guid AlbumId { get; set; }
        public IEnumerable<Guid> ArtistsIds { get; set; }
    }
}
