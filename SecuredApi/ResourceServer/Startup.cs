using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;
using ResourceServer;

[assembly: OwinStartup(typeof(Startup))]

namespace ResourceServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(
                new IdentityServerBearerTokenAuthenticationOptions
                {
                    Authority = "https://secured.local:449/identityserver/core",
                    RequiredScopes = new[] { "securedapi" }
                });

            app.UseWebApi(WebApiConfig.Register());
        }
    }
}
