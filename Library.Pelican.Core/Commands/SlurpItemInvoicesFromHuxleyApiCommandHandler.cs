using BrightSword.Pegasus.API;
using BrightSword.Pegasus.Commands.Core;

using MYOB.AccountRight.SDK.Contracts.Version2.Sale;
using MYOB.AccountRight.SDK.Services.Sale;

using Pelican.Configuration;

namespace Pelican.Commands
{
    public class SlurpItemInvoicesFromHuxleyApiCommandHandler : CommandHandler<PelicanContext, SlurpItemInvoicesFromHuxleyApiCommand, SlurpItemInvoicesFromHuxleyApiCommandArgument>
    {
        protected virtual void ProcessItem(ItemInvoice item) {}

        public override void ProcessCommand(ICommand command,
                                            ICommandHandlerContext context)
        {
            InitializeHandler((SlurpItemInvoicesFromHuxleyApiCommand) command,
                              (PelicanContext) context);

            var authorizationService = new AuthorizationService();

            var apiContext = authorizationService.GetAuthorizedContext(Context,
                                                                       CommandArgument.CompanyFileId);

            new ItemInvoiceService(apiContext.ApiConfiguration,
                                   null,
                                   apiContext.KeyService).ForeachItem(apiContext.CompanyFile,
                                                                      apiContext.CompanyFileCredentials,
                                                                      ProcessItem);
        }
    }
}