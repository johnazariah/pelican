using System;

using BrightSword.Pegasus.Core.AzureTable;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Pelican.Models
{
    public class SaleableItem : TableEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public bool Active { get; set; }
    }

    public class SaleableItemTable : AzureTableWrapper<SaleableItem>
    {
        public SaleableItemTable(CloudStorageAccount cloudStorageAccount,
                                 string saleableItemTableName)
            : base(cloudStorageAccount,
                   saleableItemTableName) {}
    }
}