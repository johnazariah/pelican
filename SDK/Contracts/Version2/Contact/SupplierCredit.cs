﻿namespace MYOB.AccountRight.SDK.Contracts.Version2.Contact
{
    public class SupplierCredit
    {
        public decimal Limit { get; set; }

        public decimal Available { get; set; }

        public decimal PastDue { get; set; }
    }
}