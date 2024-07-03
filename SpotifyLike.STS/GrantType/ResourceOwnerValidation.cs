using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using SpotifyLike.STS.Data;

namespace SpotifyLike.STS.GrantType
{
    public class ResourceOwnerValidation : IResourceOwnerPasswordValidator
    {
        private readonly IIdentityRepository _identityRepository;

        public ResourceOwnerValidation(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var usuario = await _identityRepository.FindByEmailAndPasswordAsync(context.UserName, context.Password);

            if (usuario == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Usuário ou senha inválidos");
                return;
            }

            context.Result = new GrantValidationResult(usuario.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }

    }
}
