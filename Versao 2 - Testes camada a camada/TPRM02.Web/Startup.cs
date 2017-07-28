using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TPRM02.Web.Startup))]
namespace TPRM02.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
