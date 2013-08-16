using System;
using System.Linq;
using System.Security.Principal;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

using MYOB.AccountRight.SDK;
using MYOB.AccountRight.SDK.Contracts;
using MYOB.AccountRight.SDK.Services;

using Newtonsoft.Json;

using Pelican.Configuration;
using Pelican.Models;

namespace Pelican.Commands
{
    public class AuthorizationService
    {
        private readonly AuthorizedUserAndFileTable _authorizedUserAndFileTable;
        private readonly OAuthService _oauthService;
        private readonly PelicanContext _pelicanContext;
        private AuthorizedUserAndCompanyFile _authorizedUserAndCompanyFile;

        public AuthorizationService()
        {
            _pelicanContext = PelicanContext.CreateFromApplicationSettings();

            var cloudStorageAccount = new CloudStorageAccount(new StorageCredentials(_pelicanContext.StorageAccount,
                                                                                     _pelicanContext.StorageAccountKey),
                                                              true);

            var configuration = new ApiConfiguration(_pelicanContext.ClientKey,
                                                     _pelicanContext.ClientSecret,
                                                     _pelicanContext.RedirectUrl);

            _oauthService = new OAuthService(configuration);

            _authorizedUserAndFileTable = new AuthorizedUserAndFileTable(cloudStorageAccount,
                                                                         "PelicanConfiguration");

            _authorizedUserAndCompanyFile = _authorizedUserAndFileTable.RetrieveInstanceByRowKey("John Azariah");
        }

        public void SaveToken(string code,
                              IPrincipal user)
        {
            var tokens = _oauthService.GetTokens(code);

            _authorizedUserAndCompanyFile = _authorizedUserAndFileTable.EnsureInstance(new AuthorizedUserAndCompanyFile
                                                                                       {
                                                                                           UserId = user.Identity.Name,
                                                                                           UserName = user.Identity.Name,
                                                                                           OAuthToken = JsonConvert.SerializeObject(tokens),
                                                                                           CompanyFileId = _pelicanContext.CompanyFileId,
                                                                                       });
        }

        public ApiContext GetAuthorizedContext(PelicanContext pelicanContext,
                                               Guid companyFileId)
        {
            var keyService = new OAuthKeyService(this);

            var configuration = new ApiConfiguration(pelicanContext.ClientKey,
                                                     pelicanContext.ClientSecret,
                                                     pelicanContext.RedirectUrl);

            // get companyfiles
            var cfService = new CompanyFileService(configuration,
                                                   null,
                                                   keyService);
            var companyFiles = cfService.GetRange();

            // select
            var companyFile = companyFiles.FirstOrDefault(_ => _.Id == companyFileId);

            // fetch accounts
            var credentials = new CompanyFileCredentials("Administrator",
                                                         "");

            return new ApiContext
                   {
                       ApiConfiguration = configuration,
                       CompanyFileCredentials = credentials,
                       KeyService = keyService,
                       CompanyFile = companyFile,
                   };
        }

        private class OAuthKeyService : IOAuthKeyService
        {
            private readonly AuthorizationService _container;

            public OAuthKeyService(AuthorizationService container)
            {
                _container = container;
            }

            public OAuthTokens OAuthResponse
            {
                get { return JsonConvert.DeserializeObject<OAuthTokens>(_container._authorizedUserAndCompanyFile.OAuthToken); }
                set
                {
                    _container._authorizedUserAndCompanyFile.OAuthToken = JsonConvert.SerializeObject(value);
                    _container._authorizedUserAndCompanyFile = _container._authorizedUserAndFileTable.EnsureInstance(_container._authorizedUserAndCompanyFile);
                }
            }
        }
    }
}