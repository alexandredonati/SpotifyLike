using SpotifyLike.Domain.Admin.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Repository.Repository
{
    public class UsuarioAdminRepository : RepositoryBase<UsuarioAdmin>
    {
        public UsuarioAdminRepository(SpotifyLikeAdminContext context) : base(context)
        {
        }

        public UsuarioAdmin? GetByEmailAndPassword(string email, string senha)
        {
            return this.Find(x => x.Email == email &&
                                    x.Senha == senha).FirstOrDefault();
        }
    }
}
