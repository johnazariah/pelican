using System.Collections.Generic;

using Pelican.Controllers.Core;
using Pelican.Models;
using Pelican.Service;

namespace WebApp.Pelican.Api.Controllers
{
    public class CustomersController : PelicanReadController<Customer>
    {
        // GET api/values
        public override IEnumerable<Customer> Get()
        {
            return PelicanService.GetCustomers();
        }

        // GET api/values/5
        public override Customer Get(string id)
        {
            return PelicanService.GetCustomer(id);
        }
    }
}