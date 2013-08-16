using System.Web.ModelBinding;
using System.Web.Mvc;

using Pelican.Commands;

namespace WebApp.Pelican.Configuration.Controllers
{
    public class AuthorizeController : Controller
    {
        //
        // GET: /Authorize/
        public ActionResult Index([QueryString] string code)
        {
            var authorizationService = new AuthorizationService();

            authorizationService.SaveToken(code,
                                           User);
            return View();
        }
    }
}