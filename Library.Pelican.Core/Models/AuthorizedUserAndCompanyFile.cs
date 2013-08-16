using System;

using BrightSword.Pegasus.Core.AzureTable;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Pelican.Models
{
    public class AuthorizedUserAndCompanyFile : TableEntity
    {
        public AuthorizedUserAndCompanyFile()
        {
            PartitionKey = "Pelican";
        }

        public string UserId
        {
            get { return RowKey; }
            set { RowKey = value; }
        }

        public string OAuthToken { get; set; }
        public Guid CompanyFileId { get; set; }
        public string UserName { get; set; }
    }

    public class AuthorizedUserAndFileTable : AzureTableWrapper<AuthorizedUserAndCompanyFile>
    {
        public AuthorizedUserAndFileTable(CloudStorageAccount cloudStorageAccount,
                                          string tableName)
            : base(cloudStorageAccount,
                   tableName) {}
    }
}