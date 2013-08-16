using BrightSword.Pegasus.Core.AzureTable;

using Microsoft.WindowsAzure.Storage;

namespace Pelican.Models
{
    public class QuantumCustomerTable : AzureTableWrapper<QuantumCustomer>
    {
        public QuantumCustomerTable(CloudStorageAccount cloudStorageAccount,
                                    string tableName)
            : base(cloudStorageAccount,
                   tableName) {}
    }
}