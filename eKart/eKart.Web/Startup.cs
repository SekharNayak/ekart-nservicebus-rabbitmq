using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eKart.Web.Startup))]
namespace eKart.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
