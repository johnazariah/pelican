using BrightSword.Pegasus.Core.AzureTable;

using Microsoft.WindowsAzure.Storage;

namespace Pelican.Models
{
    public class PelicanSaleTable : AzureTableWrapper<PelicanSale>
    {
        public PelicanSaleTable(CloudStorageAccount cloudStorageAccount,
                                string saleTableName)
            : base(cloudStorageAccount,
                   saleTableName) {}
    }
}