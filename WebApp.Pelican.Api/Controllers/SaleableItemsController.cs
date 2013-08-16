using System.Collections.Generic;

using Pelican.Controllers.Core;
using Pelican.Models;
using Pelican.Service;

namespace WebApp.Pelican.Api.Controllers
{
    public class SaleableItemsController : PelicanReadController<PelicanSaleableItem>
    {
        // GET api/values
        public override IEnumerable<PelicanSaleableItem> Get()
        {
            return PelicanService.GetSaleableItems();
        }

        // GET api/values/5
        public override PelicanSaleableItem Get(string id)
        {
            return PelicanService.GetSaleableItem(id);
        }
    }
}

//private static readonly Dictionary<string, PelicanSaleableItem> values = new Dictionary<string, PelicanSaleableItem>
//                                                                  {
//                                                                      {
//                                                                          "5720A577-BF07-40EE-8260-765C7A2F1ACA", new PelicanSaleableItem
//                                                                                                                  {
//                                                                                                                      Active = true,
//                                                                                                                      Name = "White Bread Loaf",
//                                                                                                                      Price = 1.50M,
//                                                                                                                      PictureUrl = "http://www.bakersdelight.com.au/media/1943/white-category.jpg",
//                                                                                                                      Id = "5720A577-BF07-40EE-8260-765C7A2F1ACA",
//                                                                                                                  }
//                                                                      },
//                                                                      {
//                                                                          "C2727921-4A2A-4D86-B211-5F0338BB29F0", new PelicanSaleableItem
//                                                                                                                  {
//                                                                                                                      Active = true,
//                                                                                                                      Name = "Artisan Bread Loaf",
//                                                                                                                      Price = 4.0M,
//                                                                                                                      PictureUrl = "http://www.bakersdelight.com.au/media/137311/artisan.jpg",
//                                                                                                                      Id = "C2727921-4A2A-4D86-B211-5F0338BB29F0",
//                                                                                                                  }
//                                                                      },
//                                                                  };