using BrightSword.Pegasus.API;

namespace Pelican.Configuration
{
    public interface IPelicanConfiguration : ICloudRunnerConfiguration
    {
        string ClientId { get; }
        string ClientSecret { get; }
        string RedirectUrl { get; }
        string ApiBaseUrl { get; }
        string CustomerTableName { get; }
        string SaleTableName { get; }
        string SaleableItemTableName { get; }
        string CompanyFileId { get; }
    }
}