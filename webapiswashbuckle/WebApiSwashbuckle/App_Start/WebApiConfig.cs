using System.Web.Http;

namespace WebApiSwashbuckle
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "{controller}",
                defaults: new { controller = "ServiceDescription" });
        }
    }
}
