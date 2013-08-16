using System;

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
}