using System;

using Microsoft.WindowsAzure.Storage.Table;

using MYOB.AccountRight.SDK.Contracts.Version2;

using Newtonsoft.Json;

namespace Pelican.Models
{
    public abstract class QuantumTableEntity<T> : TableEntity
        where T : BaseEntity
    {
        protected QuantumTableEntity(T item,
                                     Guid companyFileId)
        {
            CompanyFileId = companyFileId.ToString("D");
            UID = item.UID.ToString("D");
            RowVersion = item.RowVersion;
            Document = JsonConvert.SerializeObject(item);
        }

        protected QuantumTableEntity() {}

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

        public string Document { get; set; }
    }
}