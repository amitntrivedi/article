using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GameOfLife.Startup))]
namespace GameOfLife
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
