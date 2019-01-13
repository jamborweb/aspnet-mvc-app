using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(jScore3.Startup))]
namespace jScore3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
