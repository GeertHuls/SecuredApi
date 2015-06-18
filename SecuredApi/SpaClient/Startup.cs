using Microsoft.Owin;
using Owin;
using SpaClient;

[assembly: OwinStartup(typeof(Startup))]

namespace SpaClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}
