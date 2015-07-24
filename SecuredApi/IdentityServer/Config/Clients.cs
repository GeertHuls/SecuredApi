using System.Collections.Generic;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace IdentityServer.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    Enabled = true,
                    ClientName = "spa client (implicit)",
                    ClientId = "spaclient",
                    Flow = Flows.Implicit,
                    RequireConsent = true,

                    RedirectUris = new List<string>
                    {
                        "https://secured.local:449/spaclient/#/cb/"
                    },

                    AccessTokenLifetime = 70,

                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles,
                        "securedapi"
                    }
                },

                new Client
                {
                    Enabled = true,
                    ClientName = "implicit client example",
                    ClientId = "implicitclient",
                    Flow = Flows.Implicit,
                    RequireConsent = true,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:672/index.html",
                        "http://localhost:674/callback.html",
                        "http://localhost:674/modal.html",
                        "http://localhost:674/frame.html"
                    },

                    AccessTokenLifetime = 70,

                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles,
                        "api1"
                    }
                }
            };
        }
    }
}