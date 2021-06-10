using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebDangKyKHHT.Startup))]
namespace WebDangKyKHHT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
