using System.Web.Http;

using WebSite.Models;

namespace WebSite.Controllers
{
    public class MessageController : ApiController
    {
        public void Post([FromBody] string value)
        {
            BusDriver driver = new BusDriver();
            driver.SendMessage(value);
        }
    }
}