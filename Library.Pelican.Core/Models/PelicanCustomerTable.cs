using BrightSword.Pegasus.Core.AzureTable;

using Microsoft.WindowsAzure.Storage;

namespace Pelican.Models
{
    public class PelicanCustomerTable : AzureTableWrapper<PelicanCustomer>
    {
        public PelicanCustomerTable(CloudStorageAccount cloudStorageAccount,
                                    string tableName)
            : base(cloudStorageAccount,
                   tableName) {}
    }
}