using BrightSword.Pegasus.Core.AzureTable;

using Microsoft.WindowsAzure.Storage;

namespace Pelican.Models
{
    public class QuantumItemTable : AzureTableWrapper<QuantumItem>
    {
        public QuantumItemTable(CloudStorageAccount cloudStorageAccount,
                                string tableName)
            : base(cloudStorageAccount,
                   tableName) {}
    }
}