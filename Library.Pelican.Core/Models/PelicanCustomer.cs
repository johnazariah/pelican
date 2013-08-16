using System;
using System.Linq;

using Microsoft.WindowsAzure.Storage.Table;

using MYOB.AccountRight.SDK.Contracts.Version2.Contact;

namespace Pelican.Models
{
    public class PelicanCustomer : TableEntity
    {
        public PelicanCustomer(Customer item, Guid companyFileId)
        {
            PartitionKey = companyFileId.ToString("D");
            RowKey = item.UID.ToString("D");
            Timestamp = DateTime.UtcNow;

            Id = item.UID.ToString("D");
            Name = String.IsNullOrWhiteSpace(item.CompanyName)
                       ? String.Format("{0}, {1}",
                                       item.LastName,
                                       item.FirstName)
                       : item.CompanyName;
            if (item.Addresses.Any())
            {
                var addressData = item.Addresses.First();
                Address = string.Format("{0}, {1}, {2}",
                                        addressData.Street,
                                        addressData.City,
                                        addressData.PostCode);
            }
            PictureUrl = item.Notes;
            Balance = item.CurrentBalance;
        }

        public PelicanCustomer() {}

        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PictureUrl { get; set; }

        public Decimal Balance { get; set; }
    }
}