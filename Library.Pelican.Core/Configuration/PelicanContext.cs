using System;

using BrightSword.Pegasus.Configuration;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

using Pelican.Models;

namespace Pelican.Configuration
{
    public class PelicanContext : CloudRunnerContext
    {
        public IPelicanConfiguration Configuration { get; set; }

        public PelicanContext(IPelicanConfiguration configuration)
            : base(configuration)
        {
            Configuration = configuration;
            var cloudStorageAccount = new CloudStorageAccount(new StorageCredentials(configuration.StorageAccount,
                                                                                     configuration.StorageAccountKey),
                                                              true);

            PelicanCustomerTable = new PelicanCustomerTable(cloudStorageAccount,
                                                            configuration.PelicanCustomerTableName);

            PelicanSaleTable = new PelicanSaleTable(cloudStorageAccount,
                                                    configuration.PelicanSaleTableName);

            PelicanSaleableItemTable = new PelicanSaleableItemTable(cloudStorageAccount,
                                                                    configuration.PelicanSaleableItemTableName);
            QuantumAccountTable = new QuantumAccountTable(cloudStorageAccount,
                                                          configuration.QuantumAccountTableName);
            QuantumCustomerTable = new QuantumCustomerTable(cloudStorageAccount,
                                                            configuration.QuantumCustomerTableName);
            QuantumItemInvoiceTable = new QuantumItemInvoiceTable(cloudStorageAccount,
                                                                  configuration.QuantumItemInvoiceTableName);
            QuantumItemTable = new QuantumItemTable(cloudStorageAccount,
                                                    configuration.QuantumItemTableName);
            ClientKey = configuration.ClientKey;
            ClientSecret = configuration.ClientSecret;
            RedirectUrl = configuration.RedirectUrl;
            CompanyFileId = Guid.Parse(configuration.CompanyFileId);
        }

        public QuantumItemTable QuantumItemTable { get; set; }

        public QuantumItemInvoiceTable QuantumItemInvoiceTable { get; set; }

        public QuantumCustomerTable QuantumCustomerTable { get; set; }

        public QuantumAccountTable QuantumAccountTable { get; set; }

        public string ClientKey { get; private set; }

        public string ClientSecret { get; private set; }

        public string RedirectUrl { get; private set; }

        public PelicanCustomerTable PelicanCustomerTable { get; private set; }

        public PelicanSaleTable PelicanSaleTable { get; private set; }

        public PelicanSaleableItemTable PelicanSaleableItemTable { get; private set; }

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