using BrightSword.Pegasus.Core.AzureTable;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Pelican.Models
{
    public class Sale : TableEntity
    {
        public string Id { get; set; }

        public Customer Customer { get; set; }

        public SaleableItem[] SaleableItems { get; set; }

        public bool Paid { get; set; }
    }

    public class SaleTable : AzureTableWrapper<Sale>
    {
        public SaleTable(CloudStorageAccount cloudStorageAccount,
                         string saleTableName)
            : base(cloudStorageAccount,
                   saleTableName) {}
    }
}