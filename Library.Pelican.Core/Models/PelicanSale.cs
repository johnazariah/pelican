using Microsoft.WindowsAzure.Storage.Table;

namespace Pelican.Models
{
    public class PelicanSale : TableEntity
    {
        public string Id { get; set; }

        public PelicanCustomer Customer { get; set; }

        public PelicanSaleableItem[] SaleableItems { get; set; }

        public bool Paid { get; set; }
    }
}