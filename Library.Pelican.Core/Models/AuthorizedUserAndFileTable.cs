using BrightSword.Pegasus.Core.AzureTable;

using Microsoft.WindowsAzure.Storage;

namespace Pelican.Models
{
    public class AuthorizedUserAndFileTable : AzureTableWrapper<AuthorizedUserAndCompanyFile>
    {
        public AuthorizedUserAndFileTable(CloudStorageAccount cloudStorageAccount,
                                          string tableName)
            : base(cloudStorageAccount,
                   tableName) {}
    }
}