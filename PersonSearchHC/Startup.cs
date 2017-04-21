using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersonSearchHC.Startup))]
namespace PersonSearchHC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
