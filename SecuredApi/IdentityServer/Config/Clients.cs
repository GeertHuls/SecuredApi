using System.Collections.Generic;
using Thinktecture.IdentityServer.Core.Models;

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
                        "https://secured.local:449/spaclient/#/cb"
                    },

                    AccessTokenLifetime = 60,
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
                        "http://localhost:672/index.html"
                    },

                    AccessTokenLifetime = 60,
                }
            };
        }
    }
}