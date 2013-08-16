using System;

using Microsoft.WindowsAzure.Storage.Table;

namespace Pelican.Models
{
    public class PelicanCustomer : TableEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PictureUrl { get; set; }

        public Decimal Balance { get; set; }
    }
}