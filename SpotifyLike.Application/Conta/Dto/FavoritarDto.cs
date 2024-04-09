using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Application.Conta.Dto
{
    public class FavoritarDto
    {
        public Guid idUser { get; set; }
        public Guid idSong { get; set; }
    }
}
