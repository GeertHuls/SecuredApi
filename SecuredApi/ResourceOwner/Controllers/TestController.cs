using System.Web.Http;

namespace ResourceOwner.Controllers
{
    public class TestController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(new []
                {
                    "hello",
                    "world"
                });
        } 
    }
}
