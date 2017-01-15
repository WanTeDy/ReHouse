using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReHouse.FrontEnd.Startup))]
namespace ReHouse.FrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
