using System;
using System.Security.Cryptography.X509Certificates;
using IdentityServer.WindowsAuthentication.Configuration;
using Owin;

namespace WsFederationServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWindowsAuthenticationService(new WindowsAuthenticationOptions
            {
                IdpRealm = "urn:win",
                IdpReplyUrl = "https://secured.local:449/identity/was",
                PublicOrigin = "https://secured.local:449/federationserver",
                SigningCertificate = LoadCertificate()
            });
        }

        private X509Certificate2 LoadCertificate() => new X509Certificate2(
            $@"{AppDomain.CurrentDomain.BaseDirectory}\certificates\secured.local.pfx", "");
    }
}