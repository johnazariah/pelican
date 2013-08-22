using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using BrightSword.Pegasus.API;
using BrightSword.Pegasus.Commands.Core;

using MYOB.AccountRight.SDK.Contracts.Version2.Contact;
using MYOB.AccountRight.SDK.Contracts.Version2.GeneralLedger;
using MYOB.AccountRight.SDK.Contracts.Version2.Inventory;
using MYOB.AccountRight.SDK.Contracts.Version2.Sale;
using MYOB.AccountRight.SDK.Services.GeneralLedger;
using MYOB.AccountRight.SDK.Services.Sale;

using Pelican.Configuration;
using Pelican.Models;

namespace Pelican.Commands
{
    public class PostItemInvoiceToHuxleyApiCommandHandler : CommandHandler<PelicanContext, PostItemInvoiceToHuxleyApiCommand, PostItemInvoiceToHuxleyApiCommandArgument>
    {
        private int _referenceNbr = 100;

        private TaxCode _taxCode;

        public string PostSaleInvoice(PelicanSale sale,
                                      PelicanContext context = null,
                                      Guid? companyFileId = null)
        {
            var authorizationService = new AuthorizationService();

            context = context ?? PelicanContext.CreateFromApplicationSettings();
            companyFileId = companyFileId ?? context.CompanyFileId;

            var apiContext = authorizationService.GetAuthorizedContext(context,
                                                                       companyFileId.Value);

            try
            {
                if (_taxCode == null)
                {
                    var taxCodeService = new TaxCodeService(apiContext.ApiConfiguration);
                    var taxCodes = taxCodeService.GetRange(apiContext.CompanyFile,
                                                           null,
                                                           apiContext.CompanyFileCredentials);
                    _taxCode = taxCodes.Items.First(_ => _.Code == "GST");
                }

                var service = new ItemInvoiceService(apiContext.ApiConfiguration);
                var entity = new ItemInvoice();
                entity.UID = Guid.NewGuid();
                entity.InvoiceType = InvoiceLayoutType.Item;
                entity.Customer = new CustomerLink
                                  {
                                      UID = Guid.Parse(sale.Customer.Id)
                                  };
                entity.Number = string.Format("SJ{0:D5}",
                                              _referenceNbr++);
                entity.Date = DateTime.Today;
                var invoiceLines = new List<ItemInvoiceLine>();
                for (var index = 0;
                    index < sale.SaleableItems.Count();
                    index++)
                {
                    var saleableItem = sale.SaleableItems[index];
                    var item = new ItemInvoiceLine();
                    item.Item = new ItemLink
                                {
                                    UID = Guid.Parse(saleableItem.Id)
                                };
                    item.RowID = index;
                    item.ShipQuantity = 1;
                    item.Total = Convert.ToDecimal(saleableItem.Price);
                    item.TaxCode = new TaxCodeLink
                                   {
                                       UID = _taxCode.UID
                                   };
                    invoiceLines.Add(item);
                }
                entity.Lines = invoiceLines;
                entity.TotalAmount = Convert.ToDecimal(sale.SaleableItems.Sum(_ => _.Price));
                entity.Comment = "Entered via Pelican";
                return service.Insert(apiContext.CompanyFile,
                                      entity,
                                      apiContext.CompanyFileCredentials);
            }
            catch (Exception ex)
            {
                switch (ex.GetType()
                          .Name)
                {
                    case "WebException":
                        Debug.WriteLine(FormatMessage((WebException) ex));
                        break;
                    case "ApiCommunicationException":
                        Debug.WriteLine(FormatMessage((WebException) ex.InnerException));
                        break;
                    case "ApiOperationException":
                        Debug.WriteLine(ex.Message);
                        break;
                    default:
                        Debug.WriteLine(ex.Message);
                        break;
                }
            }
            return null;
        }

        private static string FormatMessage(WebException webEx)
        {
            var responseText = new StringBuilder();
            responseText.AppendLine(webEx.Message);
            responseText.AppendLine();

            // Call method 'GetResponseStream' to obtain stream associated with the response object 
            var response = webEx.Response;
            var receiveStream = response.GetResponseStream();

            var encode = Encoding.GetEncoding("utf-8");

            // Pipe the stream to a higher level stream reader with the required encoding format. 
            var readStream = new StreamReader(receiveStream,
                                              encode);
            var read = new Char[257];

            // Read 256 charcters at a time    . 
            var count = readStream.Read(read,
                                        0,
                                        256);
            responseText.AppendLine("HTML...");

            while (count > 0)
            {
                // Dump the 256 characters on a string and display the string onto the console. 
                var str = new String(read,
                                     0,
                                     count);
                responseText.Append(str);
                count = readStream.Read(read,
                                        0,
                                        256);
            }
            responseText.Append("");

            // Release the resources of stream object.
            readStream.Dispose();

            // Release the resources of response object.
            response.Dispose();

            return responseText.ToString();
        }

        protected virtual void ProcessItem(ItemInvoice item) {}

        public override void ProcessCommand(ICommand command,
                                            ICommandHandlerContext context)
        {
            InitializeHandler((PostItemInvoiceToHuxleyApiCommand) command,
                              (PelicanContext) context);
        }
    }

#if false
    public class PostCustomerToHuxleyApiCommandHandler : CommandHandler<PelicanContext, PostCustomerToHuxleyApiCommand, PostCustomerToHuxleyApiCommandArgument>
    {
        private int _referenceNbr = 100;

        private TaxCode _taxCode;

        public string PostCustomer(PelicanCustomer instance,
                                      PelicanContext context = null,
                                      Guid? companyFileId = null)
        {
            var authorizationService = new AuthorizationService();

            context = context ?? PelicanContext.CreateFromApplicationSettings();
            companyFileId = companyFileId ?? context.CompanyFileId;

            var apiContext = authorizationService.GetAuthorizedContext(context,
                                                                       companyFileId.Value);

            try
            {
                if (_taxCode == null)
                {
                    var taxCodeService = new TaxCodeService(apiContext.ApiConfiguration);
                    var taxCodes = taxCodeService.GetRange(apiContext.CompanyFile,
                                                           null,
                                                           apiContext.CompanyFileCredentials);
                    _taxCode = taxCodes.Items.First(_ => _.Code == "GST");
                }

                var service = new CustomerService(apiContext.ApiConfiguration);
                var entity = new Customer();
                entity.UID = Guid.NewGuid();
                entity.InvoiceType = InvoiceLayoutType.Item;
                entity.Customer = new CustomerLink
                {
                    UID = Guid.Parse(instance.Customer.Id)
                };
                entity.Number = string.Format("SJ{0:D5}",
                                              _referenceNbr++);
                entity.Date = DateTime.Today;
                var invoiceLines = new List<CustomerLine>();
                for (var index = 0;
                    index < instance.SaleableItems.Count();
                    index++)
                {
                    var saleableItem = instance.SaleableItems[index];
                    var item = new CustomerLine();
                    item.Item = new ItemLink
                    {
                        UID = Guid.Parse(saleableItem.Id)
                    };
                    item.RowID = index;
                    item.ShipQuantity = 1;
                    item.Total = saleableItem.Price;
                    item.TaxCode = new TaxCodeLink
                    {
                        UID = _taxCode.UID
                    };
                    invoiceLines.Add(item);
                }
                entity.Lines = invoiceLines;
                entity.TotalAmount = instance.SaleableItems.Sum(_ => _.Price);
                entity.Comment = "Entered via Pelican";
                return service.Insert(apiContext.CompanyFile,
                                      entity,
                                      apiContext.CompanyFileCredentials);
            }
            catch (Exception ex)
            {
                switch (ex.GetType()
                          .Name)
                {
                    case "WebException":
                        Debug.WriteLine(FormatMessage((WebException)ex));
                        break;
                    case "ApiCommunicationException":
                        Debug.WriteLine(FormatMessage((WebException)ex.InnerException));
                        break;
                    case "ApiOperationException":
                        Debug.WriteLine(ex.Message);
                        break;
                    default:
                        Debug.WriteLine(ex.Message);
                        break;
                }
            }
            return null;
        }

        private static string FormatMessage(WebException webEx)
        {
            var responseText = new StringBuilder();
            responseText.AppendLine(webEx.Message);
            responseText.AppendLine();

            // Call method 'GetResponseStream' to obtain stream associated with the response object 
            var response = webEx.Response;
            var receiveStream = response.GetResponseStream();

            var encode = Encoding.GetEncoding("utf-8");

            // Pipe the stream to a higher level stream reader with the required encoding format. 
            var readStream = new StreamReader(receiveStream,
                                              encode);
            var read = new Char[257];

            // Read 256 charcters at a time    . 
            var count = readStream.Read(read,
                                        0,
                                        256);
            responseText.AppendLine("HTML...");

            while (count > 0)
            {
                // Dump the 256 characters on a string and display the string onto the console. 
                var str = new String(read,
                                     0,
                                     count);
                responseText.Append(str);
                count = readStream.Read(read,
                                        0,
                                        256);
            }
            responseText.Append("");

            // Release the resources of stream object.
            readStream.Dispose();

            // Release the resources of response object.
            response.Dispose();

            return responseText.ToString();
        }

        protected virtual void ProcessItem(Customer item) { }

        public override void ProcessCommand(ICommand command,
                                            ICommandHandlerContext context)
        {
            InitializeHandler((PostCustomerToHuxleyApiCommand)command,
                              (PelicanContext)context);
        }
    }

    public class PostCustomerToHuxleyApiCommandArgument : ICommandArgument {}

    public class PostCustomerToHuxleyApiCommand : ICommand {
        public string CommandId { get; private set; }
        public string CommandName { get; private set; }
        public string CommandArgumentTypeName { get; private set; }
        public string SerializedCommandArgument { get; private set; }
    }

#endif
}