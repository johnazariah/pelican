using System;

using Microsoft.WindowsAzure.Storage.Table;

namespace Pelican.Models
{
    public class PelicanSaleableItem : TableEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public bool Active { get; set; }
    }
}