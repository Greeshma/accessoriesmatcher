using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AccessoriesMatcherService.Startup))]

namespace AccessoriesMatcherService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}