using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthServer.Host
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource
                {
                    Name = "uhome",
                    UserClaims =
                    {
                        JwtClaimTypes.Id,
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        LinFx.Security.Claims.ClaimTypes.TenantId,
                    },
                    Scopes =
                    {
                        new Scope{ Name = "uhome" },
                        new Scope{ Name = "uhome.rke" },
                        new Scope{ Name = "uhome.o2o" },
                        new Scope{ Name = "uhome.park" },
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "uhome.web",
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "uhome", "uhome.rke", "uhome.o2o", "uhome.park"
                    },

                    RedirectUris =
                    {
                        "http://10.0.1.222",
                        "http://10.0.1.222/index.html",
                        "http://10.0.1.222/callback.html",
                        "http://10.0.1.222/silent.html",
                        "http://10.0.1.222/popup.html",
                    },
                    PostLogoutRedirectUris = { "http://10.0.1.222/index.html" },
                    AllowedCorsOrigins = { "http://10.0.1.222" },
                },

                new Client
                {
                    ClientId = "uhome.rke",
                    ClientSecrets = { new Secret("123456".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "uhome", "uhome.rke", "uhome.o2o", "uhome.park"
                    }
                },

                new Client
                {
                    ClientId = "uhome.o2o",
                    ClientSecrets = { new Secret("123456".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "uhome", "uhome.rke", "uhome.o2o", "uhome.park"
                    }
                },
            };
        }
    }
}