using SpotifyLike.Domain.Conta.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Repository.Repository
{
    public class PlaylistRepository : RepositoryBase<Playlist>
    {
        private SpotifyLikeContext Context { get; set; }
        public PlaylistRepository(SpotifyLikeContext context) : base(context)
        {
            Context = context;
        }
    }
}
