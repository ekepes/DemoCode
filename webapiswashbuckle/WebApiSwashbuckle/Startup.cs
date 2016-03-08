using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApiSwashbuckle.Startup))]

namespace WebApiSwashbuckle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
