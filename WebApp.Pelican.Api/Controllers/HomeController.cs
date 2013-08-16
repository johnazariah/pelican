using System.Web.Mvc;

namespace WebApp.Pelican.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}