using System.Collections.Generic;

using Pelican.Controllers.Core;
using Pelican.Models;
using Pelican.Service;

namespace WebApp.Pelican.Api.Controllers
{
    public class CustomersController : PelicanReadController<PelicanCustomer>
    {
        // GET api/values
        public override IEnumerable<PelicanCustomer> Get()
        {
            return PelicanService.GetCustomers();
        }

        // GET api/values/5
        public override PelicanCustomer Get(string id)
        {
            return PelicanService.GetCustomer(id);
        }
    }
}