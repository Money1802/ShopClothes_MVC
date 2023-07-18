using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(code_web_banhang.StartupOwin))]

namespace code_web_banhang
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
