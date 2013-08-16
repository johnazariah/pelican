using Microsoft.WindowsAzure.Storage.Table;

namespace Pelican.Models
{
    public abstract class QuantumTableEntity : TableEntity
    {
        public string CompanyFileId
        {
            get { return PartitionKey; }
            set { PartitionKey = value; }
        }

        public string UID
        {
            get { return RowKey; }
            set { RowKey = value; }
        }

        public string RowVersion { get; set; }
    }

    public class QuantumAccount : QuantumTableEntity { }
}