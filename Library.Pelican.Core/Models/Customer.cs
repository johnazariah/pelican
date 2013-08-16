using System;

using BrightSword.Pegasus.Core.AzureTable;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Pelican.Models
{
    public class Customer : TableEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PictureUrl { get; set; }

        public Decimal Balance { get; set; }
    }

    public class CustomerTable : AzureTableWrapper<Customer>
    {
        public CustomerTable(CloudStorageAccount cloudStorageAccount,
                             string customerTableName)
            : base(cloudStorageAccount,
                   customerTableName) {}
    }
}