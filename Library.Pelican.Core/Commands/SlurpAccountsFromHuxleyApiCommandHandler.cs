using BrightSword.Pegasus.API;
using BrightSword.Pegasus.Commands.Core;

using MYOB.AccountRight.SDK.Contracts.Version2.GeneralLedger;
using MYOB.AccountRight.SDK.Services.GeneralLedger;

using Pelican.Configuration;

namespace Pelican.Commands
{
    public class SlurpAccountsFromHuxleyApiCommandHandler : CommandHandler<PelicanContext, SlurpAccountsFromHuxleyApiCommand, SlurpAccountsFromHuxleyApiCommandArgument>
    {
        protected virtual void ProcessItem(Account item) {}

        public override void ProcessCommand(ICommand command,
                                            ICommandHandlerContext context)
        {
            InitializeHandler((SlurpAccountsFromHuxleyApiCommand) command,
                              (PelicanContext) context);

            var authorizationService = new AuthorizationService();

            var apiContext = authorizationService.GetAuthorizedContext(Context,
                                                                       CommandArgument.CompanyFileId);

            new AccountService(apiContext.ApiConfiguration,
                               null,
                               apiContext.KeyService).ForeachItem(apiContext.CompanyFile,
                                                                  apiContext.CompanyFileCredentials,
                                                                  ProcessItem);
        }
    }
}