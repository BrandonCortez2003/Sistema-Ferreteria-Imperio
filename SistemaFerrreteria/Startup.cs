using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaFerrreteria.Startup))]
namespace SistemaFerrreteria
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
