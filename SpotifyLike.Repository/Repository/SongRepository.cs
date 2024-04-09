using SpotifyLike.Domain.Streaming.Aggregates;

namespace SpotifyLike.Repository.Repository
{
    public class SongRepository : RepositoryBase<Musica>
    {
        public SongRepository(SpotifyLikeContext context) : base(context)
        {
        }
    }
}
