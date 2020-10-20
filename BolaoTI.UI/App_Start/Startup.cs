using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BolaoTI.UI.Startup))]
namespace BolaoTI.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            IoC.Start();
        }
    }
}
