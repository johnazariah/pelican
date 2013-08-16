using System;

using MYOB.AccountRight.SDK.Contracts.Version2.Contact;

namespace Pelican.Models
{
    public class QuantumCustomer : QuantumTableEntity<Customer> {
        public QuantumCustomer(Customer item,
                 Guid companyFileId)
            : base(item,
                   companyFileId) {}

        public QuantumCustomer() { }
    }
}