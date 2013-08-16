using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Pelican.Commands;

namespace WebApp.Pelican.Api
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var thread = new Thread(SyncMethod);
            thread.Start();
        }

        private void SyncMethod()
        {
            while (true)
            {
                try
                {
                    //new SlurpAccountsFromHuxleyApiCommandHandler().InsertIntoTableStorage();

                    new SlurpCustomersFromHuxleyApiCommandHandler().InsertIntoTableStorage();

                    new SlurpItemsFromHuxleyApiCommandHandler().InsertIntoTableStorage();

                }
                catch (Exception) {
                    
                    //Ignore and keep going
                }

                Thread.Sleep(10000);
            }
        }
    }
}