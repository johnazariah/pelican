using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Pelican.Data
{
    public class CustomerDataItem
    {
        public CustomerDataItem(string id, string name, string address, string pictureUrl, decimal balance)
        {
            Id = id;
            Name = name;
            Address = address;
            PictureUrl = pictureUrl;
            Balance = balance;
            PartitionKey = Guid.NewGuid().ToString();
            RowKey = Guid.NewGuid().ToString();
            ETag = Guid.NewGuid().ToString();
            Timestamp = DateTime.UtcNow;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }

        public string PictureUrl { get; private set; }

        public Decimal Balance { get; private set; }

        public string PartitionKey { get; private set; }
        public string RowKey { get; private set; }
        public string ETag { get; private set; }
        public DateTime Timestamp { get; private set; }


        [JsonIgnore]
        public string UniqueId
        {
            get { return Id; }
        }

        [JsonIgnore]
        public string Title
        {
            get { return Name; }
        }

        [JsonIgnore]
        public string Subtitle
        {
            get { return string.Format("Balance: {0:C}", Balance); }
        }

        [JsonIgnore]
        public string Description
        {
            get { return Address; }
        }

        [JsonIgnore]
        public string ImagePath
        {
            get { return PictureUrl; }
        }

        [JsonIgnore]
        public string Content
        {
            get { return "Content"; }
        }

        [JsonIgnore]
        public string CreateAnInvoice
        {
            get { return String.Format("Create an Invoice for {0}", Name); }
        }

        public override string ToString()
        {
            return Title;
        }
    }

    public class CustomerDataGroup
    {
        public CustomerDataGroup(String uniqueId, String title, String subtitle, String imagePath, String description)
        {
            UniqueId = uniqueId;
            Title = title;
            Subtitle = subtitle;
            Description = description;
            ImagePath = imagePath;
            Items = new ObservableCollection<CustomerDataItem>();
            PartitionKey = Guid.NewGuid().ToString();
            RowKey = Guid.NewGuid().ToString();
            ETag = Guid.NewGuid().ToString();
            Timestamp = DateTime.UtcNow;
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public ObservableCollection<CustomerDataItem> Items { get; private set; }

        public string PartitionKey { get; private set; }
        public string RowKey { get; private set; }
        public string ETag { get; private set; }
        public DateTime Timestamp { get; private set; }

        public override string ToString()
        {
            return Title;
        }
    }
}