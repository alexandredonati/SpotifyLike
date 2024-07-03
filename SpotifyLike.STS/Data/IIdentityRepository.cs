using SpotifyLike.STS.Model;

namespace SpotifyLike.STS.Data
{
    public interface IIdentityRepository
    {
        Task<Usuario> FindByEmailAndPasswordAsync(string email, string senha);
        Task<Usuario> FindByIdAsync(Guid id);
    }
}