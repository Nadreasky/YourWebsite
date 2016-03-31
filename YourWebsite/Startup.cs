using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YourWebsite.Startup))]
namespace YourWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
