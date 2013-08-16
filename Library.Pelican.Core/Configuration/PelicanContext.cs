using System;

using BrightSword.Pegasus.Configuration;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

using Pelican.Models;

namespace Pelican.Configuration
{
    public class PelicanContext : CloudRunnerContext
    {
        public PelicanContext(IPelicanConfiguration configuration)
            : base(configuration)
        {
            var cloudStorageAccount = new CloudStorageAccount(new StorageCredentials(configuration.StorageAccount,
                                                                                     configuration.StorageAccountKey),
                                                              true);

            CustomerTable = new CustomerTable(cloudStorageAccount,
                                              configuration.CustomerTableName);

            SaleTable = new SaleTable(cloudStorageAccount,
                                      configuration.SaleTableName);

            SaleableItemTable = new SaleableItemTable(cloudStorageAccount,
                                                      configuration.SaleableItemTableName);

            ClientKey = configuration.ClientId;
            ClientSecret = configuration.ClientSecret;
            RedirectUrl = configuration.RedirectUrl;
            CompanyFileId = Guid.Parse(configuration.CompanyFileId);
        }

        public string ClientKey { get; private set; }

        public string ClientSecret { get; private set; }

        public string RedirectUrl { get; private set; }
        public CustomerTable CustomerTable { get; private set; }

        public SaleTable SaleTable { get; private set; }

        public SaleableItemTable SaleableItemTable { get; private set; }
        public Guid CompanyFileId { get; private set; }

        public new static PelicanContext CreateFromApplicationSettings()
        {
            return new PelicanContext(PelicanConfiguration.CreateFromApplicationSettings());
        }

        public static PelicanContext CreateFromCloudConfiguration()
        {
            var config = PelicanConfiguration.CreateFromCloudConfiguration();
            return new PelicanContext(config);
        }
    }
}