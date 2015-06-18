using Microsoft.Owin;
using Owin;
using ResourceOwner;

[assembly: OwinStartup(typeof(Startup))]

namespace ResourceOwner
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(WebApiConfig.Register());
        }
    }
}
