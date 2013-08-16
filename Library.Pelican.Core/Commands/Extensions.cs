using System;

using MYOB.AccountRight.SDK;
using MYOB.AccountRight.SDK.Contracts;
using MYOB.AccountRight.SDK.Services;

namespace Pelican.Commands
{
    public static class Extensions
    {
        public static void ForeachItem<T>(this IReadable<T> service,
                                          CompanyFile companyFile,
                                          ICompanyFileCredentials credentials,
                                          Action<T> perform) where T : class
        {
            var query = "$top=999";
            do
            {
                var items = service.GetRange(companyFile,
                                             query,
                                             credentials);
                foreach (var item in items.Items)
                {
                    perform(item);
                }
                if (items.NextPageLink == null)
                {
                    break;
                }
                query = items.NextPageLink.Query;
            } while (true);
        }
    }
}