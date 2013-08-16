using System;

using BrightSword.Pegasus.API;
using BrightSword.Pegasus.Commands.Core;

using Microsoft.WindowsAzure.Storage.Table;

using MYOB.AccountRight.SDK.Contracts.Version2.Contact;
using MYOB.AccountRight.SDK.Services.Contact;

using Pelican.Configuration;
using Pelican.Models;

namespace Pelican.Commands
{
    public class SlurpCustomersFromHuxleyApiCommandHandler : CommandHandler<PelicanContext, SlurpCustomersFromHuxleyApiCommand, SlurpCustomersFromHuxleyApiCommandArgument>
    {
        public void InsertIntoTableStorage(PelicanContext context = null,
                                           Guid? companyFileId = null)
        {
            var authorizationService = new AuthorizationService();

            context = context ?? PelicanContext.CreateFromApplicationSettings();
            companyFileId = companyFileId ?? context.CompanyFileId;

            var apiContext = authorizationService.GetAuthorizedContext(context,
                                                                       companyFileId.Value);

            var service = new CustomerService(apiContext.ApiConfiguration,
                                             null,
                                             apiContext.KeyService);

            var query = "$top=99";
            do
            {
                var items = service.GetRange(apiContext.CompanyFile,
                                             query,
                                             apiContext.CompanyFileCredentials);

                var quantumTableBatchOperation = new TableBatchOperation();
                var pelicanTableBatchOperation = new TableBatchOperation();
                foreach (var item in items.Items)
                {
                    quantumTableBatchOperation.InsertOrMerge(new QuantumCustomer(item,
                                                                      companyFileId.Value));

                    pelicanTableBatchOperation.InsertOrMerge(new PelicanCustomer(item, companyFileId.Value));
                }
                context.QuantumCustomerTable.Table.ExecuteBatch(quantumTableBatchOperation);
                context.PelicanCustomerTable.Table.ExecuteBatch(pelicanTableBatchOperation);

                if (items.NextPageLink == null)
                {
                    break;
                }
                query = items.NextPageLink.Query;
            } while (true);
        }

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