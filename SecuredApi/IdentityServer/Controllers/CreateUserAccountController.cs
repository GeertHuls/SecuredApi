using System.Web.Mvc;

namespace IdentityServer.Controllers
{
    public class CreateUserAccountController : Controller
    {
        [HttpGet]
        public ActionResult Index(string signin)
        {
            return View();
        }
    }
}