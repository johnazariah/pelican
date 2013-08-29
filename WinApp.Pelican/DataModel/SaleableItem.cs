using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace WebApp.Pelican.Api.Models
{
    public class SaleableItemDataItem
    {
        public SaleableItemDataItem(string id, string name, decimal price, string pictureUrl, bool active)
        {
            Id = id;
            Name = name;
            Price = price;
            PictureUrl = pictureUrl;
            Active = active;
            PartitionKey = Guid.NewGuid().ToString();
            RowKey = Guid.NewGuid().ToString();
            ETag = Guid.NewGuid().ToString();
            Timestamp = DateTime.UtcNow;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }

        [JsonIgnore]
        public string PriceText
        {
            get { return String.Format("Price: {0:C}", Price); }
        }

        public string PictureUrl { get; set; }
        public bool Active { get; set; }
        public string PartitionKey { get; private set; }
        public string RowKey { get; private set; }
        public string ETag { get; private set; }
        public DateTime Timestamp { get; private set; }
    }

    public class SaleableItemDataGroup
    {
        public SaleableItemDataGroup(String uniqueId, String title, String subtitle, String imagePath,
            String description)
        {
            UniqueId = uniqueId;
            Title = title;
            Subtitle = subtitle;
            Description = description;
            ImagePath = imagePath;
            Items = new ObservableCollection<SaleableItemDataItem>();
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public ObservableCollection<SaleableItemDataItem> Items { get; private set; }

        public override string ToString()
        {
            return Title;
        }
    }
}