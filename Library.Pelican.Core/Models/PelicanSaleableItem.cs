using System;

using Microsoft.WindowsAzure.Storage.Table;

using MYOB.AccountRight.SDK.Contracts.Version2.Inventory;

namespace Pelican.Models
{
    public class PelicanSaleableItem : TableEntity
    {
        public PelicanSaleableItem(Item item, Guid companyFileId)
        {
            PartitionKey = companyFileId.ToString("D");
            Id = item.UID.ToString("D");
            RowKey = Id;
            Name = item.Name;
            Price = item.BaseSellingPrice;
            PictureUrl = item.Description;
            Active = item.IsActive;
        }

        public PelicanSaleableItem() {}

        public string Id { get; set; }

        public string Name { get; set; }

        public Decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public bool Active { get; set; }
    }
}