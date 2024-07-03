using Microsoft.Extensions.Options;
using SpotifyLike.STS.Model;
using System.Data.SqlClient;
using Dapper;

namespace SpotifyLike.STS.Data
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly string connectionString;

        public IdentityRepository(IOptions<DatabaseOptions> options)
        {
            this.connectionString = options.Value.SpotifyConnection;
        }

        public async Task<Usuario?> FindByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Usuario>(IdentityQuery.FindById(), new
            {
                id = id
            });
        }

        public async Task<Usuario?> FindByEmailAndPasswordAsync(string email, string pwd)
        {
            var senha = new Senha(pwd);
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Usuario>(IdentityQuery.FindByEmailAndPassword(), new
            {
                email = email,
                senha = senha.HexValue
            });
        }
    }

    public static class IdentityQuery
    {
        public static string FindById() => @"
            SELECT
                Id,
                Nome,
                Email
            FROM
                USUARIO
            WHERE
                Id = @id";

        public static string FindByEmailAndPassword() => @"
            SELECT
                Id,
                Nome,
                Email
            FROM
                USUARIO
            WHERE
                Email = @email
            AND
                SenhaHex = @senha";
    }
}
