using SpotifyLike.Domain.Streaming.ValueObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Streaming.Dto
{
    public class AlbumDto
    {
        public Guid Id { get; set; }

        [Required]
        public IEnumerable<Guid> ArtistIds { get; set; }

        [Required]
        public string Nome { get; set; }

        public List<MusicDto> Musicas { get; set; } = new List<MusicDto>();
    }

    public class MusicDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Duracao Duracao { get; set; }
    }
}
