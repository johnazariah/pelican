using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Pelican.Annotations;
using Pelican.Data;

namespace WebApp.Pelican.Api.Models
{
    public class Sale : INotifyPropertyChanged
    {
        public Sale()
        {
            PartitionKey = Guid.NewGuid().ToString();
            RowKey = Guid.NewGuid().ToString();
            ETag = Guid.NewGuid().ToString();
            Timestamp = DateTime.UtcNow;
            SaleableItems = new ObservableCollection<SaleableItemDataItem>();
            SaleableItems.CollectionChanged += SaleableItems_CollectionChanged;
        }

        public string Id { get; set; }

        public string PartitionKey { get; private set; }
        public string RowKey { get; private set; }
        public string ETag { get; private set; }
        public DateTime Timestamp { get; private set; }

        public CustomerDataItem Customer { get; set; }

        public ObservableCollection<SaleableItemDataItem> SaleableItems { get; private set; }

        public bool Paid { get; set; }

        public String InvoiceTotal
        {
            get { return String.Format("Invoice Total: {0:C}", SaleableItems.Sum(_ => _.Price)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SaleableItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("InvoiceTotal");
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}