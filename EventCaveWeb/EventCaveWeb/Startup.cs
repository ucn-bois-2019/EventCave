using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventCaveWeb.Startup))]
namespace EventCaveWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
