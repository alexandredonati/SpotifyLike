using Microsoft.Extensions.Configuration;
using SpotifyLike.Domain.Admin.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyLike.Repository.Repository
{
    public class UsuarioAdminRepository : CosmosDBContext
    {
        public UsuarioAdminRepository(IConfiguration configuration): base(configuration)
        {
            this.SetContainer("users");
        }

        public async Task<UsuarioAdmin?> GetByEmailAndPassword(string email, string senha)
        {
            var users = await this.ReadAllItems<UsuarioAdmin>();
            return users.FirstOrDefault(x => x.Email == email &&
                                    x.Senha == senha);
        }
    }
}
