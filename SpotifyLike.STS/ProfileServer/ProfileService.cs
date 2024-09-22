using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using SpotifyLike.STS.Data;
using System.Security.Claims;

namespace SpotifyLike.STS.ProfileServer
{
    public class ProfileService : IProfileService
    {
        private readonly IIdentityRepository _identityRepository;
        public ProfileService(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var id = context.Subject.GetSubjectId();
            var user = await _identityRepository.FindByIdAsync(new Guid(id));

            var claims = new List<Claim>()
            {
                new Claim("name", user.Nome),
                new Claim("email", user.Email),
                new Claim("role", "spotifylike-user")
            };

            context.IssuedClaims = claims;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}
