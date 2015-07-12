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
            app.UseWebApi(WebApiConfig.Register());
        }
    }
}
