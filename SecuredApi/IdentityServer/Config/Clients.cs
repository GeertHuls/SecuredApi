using System.Collections.Generic;
using System.Configuration;
using IdentityServer3.Core;
using IdentityServer3.Core.Models;

namespace IdentityServer.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            var spaClientUrl = ConfigurationManager.AppSettings["spaClientUrl"];

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
                        string.Format("{0}/#/cb/", spaClientUrl),
                        string.Format("{0}/app/views/frame.html", spaClientUrl)
                    },
                    
                    PostLogoutRedirectUris = new List<string>
                    {
                        string.Format("{0}/", spaClientUrl),
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
                }
            };
        }
    }
}