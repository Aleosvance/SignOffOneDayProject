using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebsiteFrontEnd.Startup))]
namespace WebsiteFrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
