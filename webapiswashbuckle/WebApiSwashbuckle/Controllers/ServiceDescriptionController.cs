using System.Web.Http;
using System.Web.Http.Description;

namespace WebApiSwashbuckle.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ServiceDescriptionController : ApiController
    {
        [HttpGet]
        [Route("")]
        public string Describe()
        {
            return "This is a description.";
        }
    }
}
