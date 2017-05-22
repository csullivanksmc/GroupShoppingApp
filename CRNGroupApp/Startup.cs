using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRNGroupApp.Startup))]
namespace CRNGroupApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
