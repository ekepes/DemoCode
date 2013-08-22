using System.Web.Http;
using System.Web.Routing;

using EventyStoreySitey.App_Start;

namespace EventyStoreySitey
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}