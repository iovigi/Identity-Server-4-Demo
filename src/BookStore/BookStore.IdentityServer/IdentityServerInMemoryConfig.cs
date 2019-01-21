namespace BookStore.IdentityServer
{
    using System.Collections.Generic;
    using IdentityModel;
    using IdentityServer4.Models;
    using IdentityServer4.Test;

    public static class IdentityServerInMemoryConfig
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",
                    Claims =
                    {
                        new System.Security.Claims.Claim(JwtClaimTypes.Name,"Alice"),
                        new System.Security.Claims.Claim(JwtClaimTypes.Email,"Alice@test.com"),
                        new System.Security.Claims.Claim("office_number", "test")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",
                    Claims =
                    {
                        new System.Security.Claims.Claim(JwtClaimTypes.Name,"Alice"),
                        new System.Security.Claims.Claim(JwtClaimTypes.Email,"Alice@test.com"),
                        new System.Security.Claims.Claim("office_number", "test")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResource()
                {
                    Name="office",
                    UserClaims = { "office_number"}
                }
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client()//Client is consumer of API
                {
                    ClientId = "BookClient",
                    ClientName="Book demo",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris={ "http://localhost:57677/signin-oidc" },
                    AllowedScopes= { "openid", "email" }//Scopes of access
                },
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                }
            };
        }
    }
}
