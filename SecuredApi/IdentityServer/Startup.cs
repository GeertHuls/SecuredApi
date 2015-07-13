using System;
using System.Security.Cryptography.X509Certificates;
using IdentityServer;
using IdentityServer.Config;
using Microsoft.Owin;
using Owin;
using Thinktecture.IdentityServer.Core.Configuration;

[assembly: OwinStartup(typeof(Startup))]

namespace IdentityServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/core", idsrvApp =>
            {
                idsrvApp.UseIdentityServer(
                    new IdentityServerOptions
                    {
                        SiteName = "SecuredApi IdentityServer",
                        IssuerUri = "https://securedapiidsrv/embedded",
                        SigningCertificate = LoadCertificate(),

                        Factory = InMemoryFactory.Create(
                            Users.Get(),
                            Clients.Get(),
                            Scopes.Get()
                        )
                    });
            });
        }

        private X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\Certificates\secured.local.pfx",
                AppDomain.CurrentDomain.BaseDirectory), "");
        }
    }
}
