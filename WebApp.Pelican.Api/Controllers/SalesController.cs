using System.Collections.Generic;
using System.Web.Http;

using Pelican.Controllers.Core;
using Pelican.Models;
using Pelican.Service;

namespace WebApp.Pelican.Api.Controllers
{
    public class SalesController : PelicanReadWriteController<Sale>
    {
        // GET api/values
        public override IEnumerable<Sale> Get()
        {
            return PelicanService.GetSales();
        }

        // GET api/values/5
        public override Sale Get(string id)
        {
            return PelicanService.GetSale(id);
        }

        // PUT api/values/5
        public override void Put([FromBody] Sale value)
        {
            PelicanService.AddSale(value);
        }
    }
}