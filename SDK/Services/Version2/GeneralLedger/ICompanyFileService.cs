using System;
using System.Net;
using MYOB.AccountRight.SDK.Contracts;

namespace MYOB.AccountRight.SDK.Services.GeneralLedger
{
    public interface ICompanyFileService
    {
        void GetRange(Action<HttpStatusCode, CompanyFile[]> onComplete, Action<Uri, Exception> onError);
        CompanyFile[] GetRange();
        void Get(CompanyFile cf, ICompanyFileCredentials credentials, Action<HttpStatusCode, CompanyFileWithResources> onComplete, Action<Uri, Exception> onError);
        CompanyFileWithResources Get(CompanyFile cf, ICompanyFileCredentials credentials);
    }
}