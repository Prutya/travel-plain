using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TravelPlain.Startup))]
namespace TravelPlain
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
