using System.Web.Mvc;

namespace Hal.PlayAround.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}