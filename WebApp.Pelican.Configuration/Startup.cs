using Owin;

namespace WebApp.Pelican.Configuration
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
