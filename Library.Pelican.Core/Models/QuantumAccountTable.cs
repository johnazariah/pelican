using BrightSword.Pegasus.Core.AzureTable;

using Microsoft.WindowsAzure.Storage;

namespace Pelican.Models
{
    public class QuantumAccountTable : AzureTableWrapper<QuantumAccount>
    {
        public QuantumAccountTable(CloudStorageAccount cloudStorageAccount,
                                   string tableName)
            : base(cloudStorageAccount,
                   tableName) {}
    }
}