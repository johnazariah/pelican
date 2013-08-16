using MYOB.AccountRight.SDK;
using MYOB.AccountRight.SDK.Contracts;

namespace Pelican.Commands
{
    public class ApiContext
    {
        public ApiConfiguration ApiConfiguration { get; set; }
        public IOAuthKeyService KeyService { get; set; }
        public CompanyFileCredentials CompanyFileCredentials { get; set; }
        public CompanyFile CompanyFile { get; set; }
    }
}