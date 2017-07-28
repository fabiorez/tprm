using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TPRM.MVC.Startup))]
namespace TPRM.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
