using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IncandescentDesigns.Startup))]
namespace IncandescentDesigns
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
