using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TravelPlain.Web.Startup))]
namespace TravelPlain.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
