using BrightSword.Pegasus.API;
using BrightSword.Pegasus.Commands.Core;

using MYOB.AccountRight.SDK.Contracts.Version2.Inventory;
using MYOB.AccountRight.SDK.Services.Inventory;

using Pelican.Configuration;

namespace Pelican.Commands
{
    public class SlurpItemsFromHuxleyApiCommandHandler : CommandHandler<PelicanContext, SlurpItemsFromHuxleyApiCommand, SlurpItemsFromHuxleyApiCommandArgument>
    {
        protected virtual void ProcessItem(Item item) {}

        public override void ProcessCommand(ICommand command,
                                            ICommandHandlerContext context)
        {
            InitializeHandler((SlurpItemsFromHuxleyApiCommand) command,
                              (PelicanContext) context);

            var authorizationService = new AuthorizationService();

            var apiContext = authorizationService.GetAuthorizedContext(Context,
                                                                       CommandArgument.CompanyFileId);

            new ItemService(apiContext.ApiConfiguration,
                            null,
                            apiContext.KeyService).ForeachItem(apiContext.CompanyFile,
                                                               apiContext.CompanyFileCredentials,
                                                               ProcessItem);
        }
    }
}