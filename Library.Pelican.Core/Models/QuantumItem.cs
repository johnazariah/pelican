using System;

using MYOB.AccountRight.SDK.Contracts.Version2.Inventory;

namespace Pelican.Models
{
    public class QuantumItem : QuantumTableEntity<Item> {
        public QuantumItem(Item item,
                 Guid companyFileId)
            : base(item,
                   companyFileId) {}
         public QuantumItem() {}
    }
}