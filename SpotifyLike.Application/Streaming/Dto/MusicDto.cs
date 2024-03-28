using SpotifyLike.Domain.Streaming.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Streaming.Dto
{
    public class MusicDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public Duracao Duracao { get; set; }
    }
}
