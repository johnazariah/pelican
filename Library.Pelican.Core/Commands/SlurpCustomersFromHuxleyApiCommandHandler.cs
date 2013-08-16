using BrightSword.Pegasus.API;
using BrightSword.Pegasus.Commands.Core;

using MYOB.AccountRight.SDK.Contracts.Version2.Contact;
using MYOB.AccountRight.SDK.Services.Contact;

using Pelican.Configuration;

namespace Pelican.Commands
{
    public class SlurpCustomersFromHuxleyApiCommandHandler : CommandHandler<PelicanContext, SlurpCustomersFromHuxleyApiCommand, SlurpCustomersFromHuxleyApiCommandArgument>
    {
        protected virtual void ProcessItem(Customer item) {}

        public override void ProcessCommand(ICommand command,
                                            ICommandHandlerContext context)
        {
            InitializeHandler((SlurpCustomersFromHuxleyApiCommand) command,
                              (PelicanContext) context);

            var authorizationService = new AuthorizationService();

            var apiContext = authorizationService.GetAuthorizedContext(Context,
                                                                       CommandArgument.CompanyFileId);

            new CustomerService(apiContext.ApiConfiguration,
                                null,
                                apiContext.KeyService).ForeachItem(apiContext.CompanyFile,
                                                                   apiContext.CompanyFileCredentials,
                                                                   ProcessItem);
        }
    }
}