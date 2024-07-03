﻿using IdentityServer4;
using IdentityServer4.Models;

namespace SpotifyLike.STS
{
    public class IdentityServerConfiguration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("Spotifylike-api", "SpotifyLike", new string[] { "spotifylike-user"})
                {
                    ApiSecrets = { new Secret("SpotifyLikeSecret".Sha256()) },
                    Scopes = { "SpotifylikeScope" }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope()
                {
                    Name = "SpotifylikeScope",
                    DisplayName = "SpotifyLike API",
                    UserClaims = { "spotifylike-user" } 
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client-angular-spotify",
                    ClientName = "Acesso Front APIs",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets = { new Secret("SpotifyLikeSecret".Sha256()) },
                    AllowedScopes = 
                    { 
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "SpotifylikeScope" 
                    }
                }
            };
        }
    }
}
