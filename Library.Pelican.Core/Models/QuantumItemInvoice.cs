using System;

using MYOB.AccountRight.SDK.Contracts.Version2.Sale;

namespace Pelican.Models
{
    public class QuantumItemInvoice : QuantumTableEntity<ItemInvoice>
    {
        public QuantumItemInvoice(ItemInvoice item,
                                  Guid companyFileId)
            : base(item,
                   companyFileId) {}

        public QuantumItemInvoice() {}
    }
}