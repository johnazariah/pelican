using System;

using BrightSword.Pegasus.API;
using BrightSword.Pegasus.Commands.Core;

using Microsoft.WindowsAzure.Storage.Table;

using MYOB.AccountRight.SDK.Contracts.Version2.GeneralLedger;
using MYOB.AccountRight.SDK.Services.GeneralLedger;

using Pelican.Configuration;
using Pelican.Models;

namespace Pelican.Commands
{
    public class SlurpAccountsFromHuxleyApiCommandHandler : CommandHandler<PelicanContext, SlurpAccountsFromHuxleyApiCommand, SlurpAccountsFromHuxleyApiCommandArgument>,
                                                            IHuxleyApiSlurper<Account>
    {
        public void Slurp(Action<Account> processItem,
                          PelicanContext context = null,
                          Guid? companyFileId = null)
        {
            var authorizationService = new AuthorizationService();

            context = context ?? PelicanContext.CreateFromApplicationSettings();
            companyFileId = companyFileId ?? context.CompanyFileId;

            var apiContext = authorizationService.GetAuthorizedContext(context,
                                                                       companyFileId.Value);

            new AccountService(apiContext.ApiConfiguration,
                               null,
                               apiContext.KeyService).ForeachItem(apiContext.CompanyFile,
                                                                  apiContext.CompanyFileCredentials,
                                                                  processItem);
        }

        public void InsertIntoTableStorage(PelicanContext context = null,
                                           Guid? companyFileId = null)
        {
            var authorizationService = new AuthorizationService();

            context = context ?? PelicanContext.CreateFromApplicationSettings();
            companyFileId = companyFileId ?? context.CompanyFileId;

            var apiContext = authorizationService.GetAuthorizedContext(context,
                                                                       companyFileId.Value);

            var service = new AccountService(apiContext.ApiConfiguration,
                                             null,
                                             apiContext.KeyService);

            var query = "$top=99";
            do
            {
                var items = service.GetRange(apiContext.CompanyFile,
                                             query,
                                             apiContext.CompanyFileCredentials);

                var quantumTableBatchOperation = new TableBatchOperation();
                foreach (var item in items.Items)
                {
                    quantumTableBatchOperation.InsertOrMerge(new QuantumAccount(item,
                                                                                companyFileId.Value));
                }
                context.QuantumAccountTable.Table.ExecuteBatch(quantumTableBatchOperation);

                if (items.NextPageLink == null)
                {
                    break;
                }
                query = items.NextPageLink.Query;
            } while (true);
        }

        protected virtual void ProcessItem(Account item) {}

        public override void ProcessCommand(ICommand command,
                                            ICommandHandlerContext context)
        {
            InitializeHandler((SlurpAccountsFromHuxleyApiCommand) command,
                              (PelicanContext) context);

            Slurp(ProcessItem,
                  Context,
                  CommandArgument.CompanyFileId);
        }
    }
}