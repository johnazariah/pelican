using System;

using BrightSword.Pegasus.API;
using BrightSword.Pegasus.Commands.Core;

using MYOB.AccountRight.SDK.Services.GeneralLedger;

using Pelican.Configuration;

namespace Pelican.Commands
{
    public class SlurpAccountsFromHuxleyApiCommandHandler : CommandHandler<PelicanContext, SlurpAccountsFromHuxleyApiCommand, SlurpAccountsFromHuxleyApiCommandArgument>
    {
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
                                                                  _ => Console.WriteLine("{0} ({1}) :: {2}-{3:d4} [{4}]",
                                                                                         _.UID,
                                                                                         _.RowVersion,
                                                                                         _.Type,
                                                                                         _.Number,
                                                                                         _.Name));
        }
    }
}