using BrightSword.Pegasus.Core.AzureTable;

using Microsoft.WindowsAzure.Storage;

namespace Pelican.Models
{
    public class PelicanSaleableItemTable : AzureTableWrapper<PelicanSaleableItem>
    {
        public PelicanSaleableItemTable(CloudStorageAccount cloudStorageAccount,
                                        string saleableItemTableName)
            : base(cloudStorageAccount,
                   saleableItemTableName) {}
    }
}