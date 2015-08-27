using System;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using IdentityServer;
using IdentityServer.Config;
using IdentityServer.Helpers;
using IdentityServer3.Core;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using Microsoft.Owin;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace IdentityServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/core", idsrvApp =>
            {
                var identityServerServiceFactory = new IdentityServerServiceFactory()
                    .UseInMemoryUsers(Users.Get())
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get());

                identityServerServiceFactory.CorsPolicyService =
                    new Registration<ICorsPolicyService>(
                        new DefaultCorsPolicyService
                        {
                            AllowAll = true
                        });

                var identityServerOptions = new IdentityServerOptions
                {
                    SiteName = "SecuredApi IdentityServer",
                    IssuerUri = "https://securedapiidsrv/embedded",
                    SigningCertificate = LoadCertificate(),

                    AuthenticationOptions = new AuthenticationOptions
                    {
                        IdentityProviders = ConfigureIdentityProviders,
                        EnablePostSignOutAutoRedirect = true
                    },

                    Factory = identityServerServiceFactory
                };

                idsrvApp.UseIdentityServer(identityServerOptions);
            });
        }

        private void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            var aad = new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = "aad",
                Caption = "Azure AD",
                SignInAsAuthenticationType = signInAsType,

                Authority = "https://login.windows.net/xxxxxxxxxxxxxxx",
                ClientId = "cdb67f7b-0a52-450b-a555-fe01ee12abc2",
                RedirectUri = "https://secured.local:449/identityserver/core/aadcb",
                PostLogoutRedirectUri = "https://secured.local:449/spaclient",
                Scope = "openid profile email roles",
                
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async n =>
                    {
                        var email = GetEmailAddress(n.ProtocolMessage.IdToken);
                        var role = ResolveRole(email);

                        if (role != null)
                        {
                            n.AuthenticationTicket.Identity.AddClaim(role);
                        }
                    }
                }
            };

            app.UseOpenIdConnectAuthentication(aad);
        }

        private static string GetEmailAddress(string idToken)
        {
            var claims = TokenHelper.Decocde(idToken);
            return claims.Value<string>(Constants.ClaimTypes.Email)
                ?? claims.Value<string>("unique_name");
        }

        private static Claim ResolveRole(string email)
        {
            if (email.Equals("geert.huls@outlook.com", StringComparison.InvariantCultureIgnoreCase))
            {
                return new Claim(Constants.ClaimTypes.Role, "Books");
            }

            return email.Equals("geert.huls82@gmail.com", StringComparison.InvariantCultureIgnoreCase)
                ? new Claim(Constants.ClaimTypes.Role, "Movies")
                : null;
        }

        private X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\Certificates\secured.local.pfx",
                AppDomain.CurrentDomain.BaseDirectory), "");
        }
    }
}
