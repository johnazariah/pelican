using System;

using Microsoft.WindowsAzure;

using Pelican.Properties;

namespace Pelican.Configuration
{
    public class PelicanConfiguration : IPelicanConfiguration
    {
        private PelicanConfiguration() {}
        public string ClientKey { get; private set; }
        public string StorageAccount { get; private set; }
        public string StorageAccountKey { get; private set; }
        public string RegisteredCommandHandlersTableName { get; private set; }
        public string RegisteredCommandHandlersBlobContainerName { get; private set; }
        public string CommandQueueName { get; private set; }
        public string CommandResultsTableName { get; private set; }

        public string ClientSecret { get; private set; }
        public string RedirectUrl { get; private set; }
        public string ApiBaseUrl { get; private set; }
        public string PelicanCustomerTableName { get; private set; }
        public string PelicanSaleTableName { get; private set; }
        public string PelicanSaleableItemTableName { get; private set; }
        public string QuantumCustomerTableName { get; private set; }
        public string QuantumItemTableName { get; private set; }
        public string QuantumAccountTableName { get; private set; }
        public string QuantumItemInvoiceTableName { get; private set; }
        public string CompanyFileId { get; private set; }

        public static PelicanConfiguration CreateFromApplicationSettings()
        {
            var rawApiBaseUrl = Settings.Default.ApiBaseUrl;

            var apiBaseUrl = String.IsNullOrWhiteSpace(rawApiBaseUrl)
                                 ? null
                                 : rawApiBaseUrl;

            return new PelicanConfiguration
                   {
                       ClientKey = Settings.Default.ClientKey,
                       ClientSecret = Settings.Default.ClientSecret,
                       RedirectUrl = Settings.Default.RedirectUrl,
                       ApiBaseUrl = apiBaseUrl,
                       StorageAccount = Settings.Default.StorageAccount,
                       StorageAccountKey = Settings.Default.StorageAccountKey,
                       RegisteredCommandHandlersTableName = Settings.Default.RegisteredCommandHandlersTableName,
                       RegisteredCommandHandlersBlobContainerName = Settings.Default.RegisteredCommandHandlersBlobContainerName,
                       CommandQueueName = Settings.Default.CommandQueueName,
                       CommandResultsTableName = Settings.Default.CommandResultsTableName,
                       PelicanCustomerTableName = Settings.Default.PelicanCustomerTableName,
                       PelicanSaleTableName = Settings.Default.PelicanSaleTableName,
                       PelicanSaleableItemTableName = Settings.Default.PelicanSaleableItemTableName,
                       QuantumAccountTableName = Settings.Default.QuantumAccountTableName,
                       QuantumCustomerTableName = Settings.Default.QuantumCustomerTableName,
                       QuantumItemInvoiceTableName = Settings.Default.QuantumItemInvoiceTableName,
                       QuantumItemTableName = Settings.Default.QuantumItemTableName,
                       CompanyFileId = Settings.Default.CompanyFileId
                   };
        }

        public static PelicanConfiguration CreateFromCloudConfiguration()
        {
            var rawApiBaseUrl = CloudConfigurationManager.GetSetting("ApiBaseUrl");
            var apiBaseUrl = String.IsNullOrWhiteSpace(rawApiBaseUrl)
                                 ? null
                                 : rawApiBaseUrl;

            return new PelicanConfiguration
                   {
                       ClientKey = CloudConfigurationManager.GetSetting("ClientKey"),
                       ClientSecret = CloudConfigurationManager.GetSetting("ClientSecret"),
                       RedirectUrl = CloudConfigurationManager.GetSetting("RedirectUrl"),
                       ApiBaseUrl = apiBaseUrl,
                       StorageAccount = CloudConfigurationManager.GetSetting("StorageAccount"),
                       StorageAccountKey = CloudConfigurationManager.GetSetting("StorageAccountKey"),
                       RegisteredCommandHandlersTableName = CloudConfigurationManager.GetSetting("RegisteredCommandHandlersTableName"),
                       RegisteredCommandHandlersBlobContainerName = CloudConfigurationManager.GetSetting("RegisteredCommandHandlersBlobContainerName"),
                       CommandQueueName = CloudConfigurationManager.GetSetting("CommandQueueName"),
                       CommandResultsTableName = CloudConfigurationManager.GetSetting("CommandResultsTableName"),
                       PelicanCustomerTableName = CloudConfigurationManager.GetSetting("PelicanCustomerTableName"),
                       PelicanSaleTableName = CloudConfigurationManager.GetSetting("PelicanSaleTableName"),
                       PelicanSaleableItemTableName = CloudConfigurationManager.GetSetting("PelicanSaleableItemTableName"),
                       QuantumAccountTableName = CloudConfigurationManager.GetSetting("QuantumAccountTableName"),
                       QuantumCustomerTableName = CloudConfigurationManager.GetSetting("QuantumCustomerTableName"),
                       QuantumItemInvoiceTableName = CloudConfigurationManager.GetSetting("QuantumItemInvoiceTableName"),
                       QuantumItemTableName = CloudConfigurationManager.GetSetting("QuantumItemTableName"),
                       CompanyFileId = CloudConfigurationManager.GetSetting("CompanyFileId"),
                   };
        }
    }
}