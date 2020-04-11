using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(OMW_Project.Startup))]
namespace OMW_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
