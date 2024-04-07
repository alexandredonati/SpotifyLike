using SpotifyLike.Domain.Transacao.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Repository.Repository
{
    public class TransacaoRepository : RepositoryBase<Transacao>
    {
        public SpotifyLikeContext Context { get; set; }
        public TransacaoRepository(SpotifyLikeContext context) : base(context)
        {
            Context = context;
        }
    }
}
