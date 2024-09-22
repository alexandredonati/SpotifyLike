using SpotifyLike.Domain.Conta.Aggregates;

namespace SpotifyLike.Repository.Repository
{
    public class AssinaturaRepository: RepositoryBase<Assinatura>
    {
        public SpotifyLikeContext Context { get; set; }

        public AssinaturaRepository(SpotifyLikeContext context) : base(context)
        {
            Context = context;
        }
    }
}
