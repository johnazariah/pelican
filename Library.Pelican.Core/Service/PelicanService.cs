using System.Collections.Generic;
using System.Linq;

using Microsoft.WindowsAzure.Storage.Table;

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

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.CustomerTable.GetInstances();
        }

        public Customer GetCustomer(string id)
        {
            var query = new TableQuery<Customer>().Where(TableQuery.GenerateFilterCondition("RowKey",
                                                                                            QueryComparisons.Equal,
                                                                                            id));
            return _context.CustomerTable.GetInstances(query)
                           .SingleOrDefault();
        }

        public IEnumerable<SaleableItem> GetSaleableItems()
        {
            return _context.SaleableItemTable.GetInstances();
        }

        public IEnumerable<Sale> GetSales()
        {
            return _context.SaleTable.GetInstances();
        }

        public Sale GetSale(string id)
        {
            var query = new TableQuery<Sale>().Where(TableQuery.GenerateFilterCondition("RowKey",
                                                                                        QueryComparisons.Equal,
                                                                                        id));
            return _context.SaleTable.GetInstances(query)
                           .SingleOrDefault();
        }

        public void AddSale(Sale sale)
        {
            _context.SaleTable.EnsureInstance(sale);
        }

        public SaleableItem GetSaleableItem(string id)
        {
            var query = new TableQuery<SaleableItem>().Where(TableQuery.GenerateFilterCondition("RowKey",
                                                                                                QueryComparisons.Equal,
                                                                                                id));
            return _context.SaleableItemTable.GetInstances(query)
                           .SingleOrDefault();
        }
    }
}