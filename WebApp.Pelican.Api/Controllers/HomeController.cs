using System;
using System.Web.Mvc;
using MYOB.AccountRight.SDK.Services.GeneralLedger;
using Pelican.Commands;
using Pelican.Configuration;

namespace WebApp.Pelican.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sync()
        {
            var authorizationService = new AuthorizationService();
            var context = PelicanContext.CreateFromApplicationSettings();


            var apiContext = authorizationService.GetAuthorizedContext(context,
                                                                       context.CompanyFileId);

            var accountService = new AccountService(apiContext.ApiConfiguration,
                null,
                apiContext.KeyService);

            accountService.ForeachItem(apiContext.CompanyFile,
                                                                  apiContext.CompanyFileCredentials,
                                                                  _ => Console.WriteLine("{0} ({1}) :: {2}-{3:d4} [{4}]",
                                                                                         _.UID,
                                                                                         _.RowVersion,
                                                                                         _.Type,
                                                                                         _.Number,
                                                                                         _.Name));

            return View("Index");
        }
    }
}