using BrightSword.Pegasus.Core.AzureTable;

using Microsoft.WindowsAzure.Storage;

namespace Pelican.Models
{
    public class QuantumItemInvoiceTable : AzureTableWrapper<QuantumItemInvoice>
    {
        public QuantumItemInvoiceTable(CloudStorageAccount cloudStorageAccount,
                                       string tableName)
            : base(cloudStorageAccount,
                   tableName) {}
    }
}