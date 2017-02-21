using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(iaservice.Startup))]

namespace iaservice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}