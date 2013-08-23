using System.Web.Http;

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