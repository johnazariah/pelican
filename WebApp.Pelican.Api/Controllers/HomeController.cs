using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Microsoft.WindowsAzure.Storage.Table;

using MYOB.AccountRight.SDK.Contracts.Version2.GeneralLedger;

using Pelican.Commands;
using Pelican.Models;

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
            new SlurpAccountsFromHuxleyApiCommandHandler().InsertIntoTableStorage();

            new SlurpCustomersFromHuxleyApiCommandHandler().InsertIntoTableStorage();

            new SlurpItemsFromHuxleyApiCommandHandler().InsertIntoTableStorage();

            return View("Index");
        }
    }
}