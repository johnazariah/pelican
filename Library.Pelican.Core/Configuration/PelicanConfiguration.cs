using Microsoft.WindowsAzure;

using Pelican.Properties;

namespace Pelican.Configuration
{
    public class PelicanConfiguration : IPelicanConfiguration
    {
        private PelicanConfiguration() {}
        public string StorageAccount { get; private set; }
        public string StorageAccountKey { get; private set; }
        public string RegisteredCommandHandlersTableName { get; private set; }
        public string RegisteredCommandHandlersBlobContainerName { get; private set; }
        public string CommandQueueName { get; private set; }
        public string CommandResultsTableName { get; private set; }

        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public string RedirectUrl { get; private set; }
        public string ApiBaseUrl { get; private set; }
        public string CustomerTableName { get; private set; }
        public string SaleTableName { get; private set; }
        public string SaleableItemTableName { get; private set; }
        public string CompanyFileId { get; private set; }

        public static PelicanConfiguration CreateFromApplicationSettings()
        {
            return new PelicanConfiguration
                   {
                       StorageAccount = Settings.Default.StorageAccount,
                       StorageAccountKey = Settings.Default.StorageAccountKey,
                       RegisteredCommandHandlersTableName = Settings.Default.RegisteredCommandHandlersTableName,
                       RegisteredCommandHandlersBlobContainerName = Settings.Default.RegisteredCommandHandlersBlobContainerName,
                       CommandQueueName = Settings.Default.CommandQueueName,
                       CommandResultsTableName = Settings.Default.CommandResultsTableName,
                       CustomerTableName = Settings.Default.CustomerTableName,
                       SaleTableName = Settings.Default.SaleTableName,
                       SaleableItemTableName = Settings.Default.SaleableItemTableName,
                       CompanyFileId = Settings.Default.CompanyFileId,
                   };
        }

        public static PelicanConfiguration CreateFromCloudConfiguration()
        {
            return new PelicanConfiguration
                   {
                       StorageAccount = CloudConfigurationManager.GetSetting("StorageAccount"),
                       StorageAccountKey = CloudConfigurationManager.GetSetting("StorageAccountKey"),
                       RegisteredCommandHandlersTableName = CloudConfigurationManager.GetSetting("RegisteredCommandHandlersTableName"),
                       RegisteredCommandHandlersBlobContainerName = CloudConfigurationManager.GetSetting("RegisteredCommandHandlersBlobContainerName"),
                       CommandQueueName = CloudConfigurationManager.GetSetting("CommandQueueName"),
                       CommandResultsTableName = CloudConfigurationManager.GetSetting("CommandResultsTableName"),
                       CustomerTableName = CloudConfigurationManager.GetSetting("CustomerTableName"),
                       SaleTableName = CloudConfigurationManager.GetSetting("SaleTableName"),
                       SaleableItemTableName = CloudConfigurationManager.GetSetting("SaleableItemTableName"),
                       CompanyFileId = CloudConfigurationManager.GetSetting("CompanyFileId"),
                   };
        }
    }
}