using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BuscaEmprego.Startup))]
namespace BuscaEmprego
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
