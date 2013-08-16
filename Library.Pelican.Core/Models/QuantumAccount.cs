using System;

using MYOB.AccountRight.SDK.Contracts.Version2.GeneralLedger;

namespace Pelican.Models
{
    public class QuantumAccount : QuantumTableEntity<Account>
    {
        public QuantumAccount(Account item,
                              Guid companyFileId)
            : base(item,
                   companyFileId) {}

        public QuantumAccount() {}
    }
}