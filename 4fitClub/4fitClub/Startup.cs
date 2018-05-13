using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_4fitClub.Startup))]
namespace _4fitClub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
