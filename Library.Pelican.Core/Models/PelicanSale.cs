using Microsoft.WindowsAzure.Storage.Table;

namespace Pelican.Models
{
    public class PelicanSale : TableEntity
    {
        public string Id { get; set; }

        public PelicanCustomer PelicanCustomer { get; set; }

        public PelicanSaleableItem[] PelicanSaleableItems { get; set; }

        public bool Paid { get; set; }
    }
}