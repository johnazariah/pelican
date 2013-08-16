using System.Collections.Generic;
using System.Linq;

using Microsoft.WindowsAzure.Storage.Table;

using Pelican.Commands;
using Pelican.Configuration;
using Pelican.Models;

namespace Pelican.Service
{
    public class PelicanService
    {
        private readonly PelicanContext _context;

        public PelicanService(PelicanContext context)
        {
            _context = context;
        }

        public IEnumerable<PelicanCustomer> GetCustomers()
        {
            return _context.PelicanCustomerTable.GetInstances();
        }

        public PelicanCustomer GetCustomer(string id)
        {
            var query = new TableQuery<PelicanCustomer>().Where(TableQuery.GenerateFilterCondition("RowKey",
                                                                                            QueryComparisons.Equal,
                                                                                            id));
            return _context.PelicanCustomerTable.GetInstances(query)
                           .SingleOrDefault();
        }

        public IEnumerable<PelicanSaleableItem> GetSaleableItems()
        {
            return _context.PelicanSaleableItemTable.GetInstances();
        }

        public IEnumerable<PelicanSale> GetSales()
        {
            return _context.PelicanSaleTable.GetInstances();
        }

        public PelicanSale GetSale(string id)
        {
            var query = new TableQuery<PelicanSale>().Where(TableQuery.GenerateFilterCondition("RowKey",
                                                                                        QueryComparisons.Equal,
                                                                                        id));
            return _context.PelicanSaleTable.GetInstances(query)
                           .SingleOrDefault();
        }

        public void AddSale(PelicanSale pelicanCustomer)
        {
            _context.PelicanSaleTable.EnsureInstance(pelicanCustomer);

            var poster = new PostItemInvoiceToHuxleyApiCommandHandler();
            poster.PostSaleInvoice(pelicanCustomer);
        }

        public PelicanSaleableItem GetSaleableItem(string id)
        {
            var query = new TableQuery<PelicanSaleableItem>().Where(TableQuery.GenerateFilterCondition("RowKey",
                                                                                                QueryComparisons.Equal,
                                                                                                id));
            return _context.PelicanSaleableItemTable.GetInstances(query)
                           .SingleOrDefault();
        }
    }
}
