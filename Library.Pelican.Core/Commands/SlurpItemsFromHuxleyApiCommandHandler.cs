using System;

using BrightSword.Pegasus.API;
using BrightSword.Pegasus.Commands.Core;

using Microsoft.WindowsAzure.Storage.Table;

using MYOB.AccountRight.SDK.Contracts.Version2.Inventory;
using MYOB.AccountRight.SDK.Services.Inventory;

using Pelican.Configuration;
using Pelican.Models;

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


        public void InsertIntoTableStorage(PelicanContext context = null,
                                           Guid? companyFileId = null)
        {
            var authorizationService = new AuthorizationService();

            context = context ?? PelicanContext.CreateFromApplicationSettings();
            companyFileId = companyFileId ?? context.CompanyFileId;

            var apiContext = authorizationService.GetAuthorizedContext(context,
                                                                       companyFileId.Value);

            var service = new ItemService(apiContext.ApiConfiguration,
                                             null,
                                             apiContext.KeyService);

            var query = "$top=99";
            do
            {
                var items = service.GetRange(apiContext.CompanyFile,
                                             query,
                                             apiContext.CompanyFileCredentials);

                var quantumBatchOperation = new TableBatchOperation();
                var pelicanBatchOperation = new TableBatchOperation();
                foreach (var item in items.Items)
                {
                    quantumBatchOperation.InsertOrMerge(new QuantumItem(item,
                                                                      companyFileId.Value));
                    pelicanBatchOperation.InsertOrMerge(new PelicanSaleableItem(item, companyFileId.Value));
                }
                context.QuantumItemTable.Table.ExecuteBatch(quantumBatchOperation);
                context.PelicanSaleableItemTable.Table.ExecuteBatch(pelicanBatchOperation);

                if (items.NextPageLink == null)
                {
                    break;
                }
                query = items.NextPageLink.Query;
            } while (true);
        }

    }
}