using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Linq.Startup))]
namespace Linq
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
