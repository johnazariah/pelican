using BrightSword.Pegasus.API;

namespace Pelican.Configuration
{
    public interface IPelicanConfiguration : ICloudRunnerConfiguration
    {
        string ClientKey { get; }
        string ClientSecret { get; }
        string RedirectUrl { get; }
        string ApiBaseUrl { get; }
        string PelicanCustomerTableName { get; }
        string PelicanSaleTableName { get; }
        string PelicanSaleableItemTableName { get; }

        string QuantumCustomerTableName { get; }
        string QuantumItemTableName { get; }
        string QuantumAccountTableName { get; }
        string QuantumItemInvoiceTableName { get; }

        string CompanyFileId { get; }
    }
}