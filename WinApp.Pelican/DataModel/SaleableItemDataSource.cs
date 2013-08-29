using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using WebApp.Pelican.Api.Models;

namespace Pelican.Data
{
    public class SaleableItemDataSource
    {
        private static readonly SaleableItemDataSource _saleableItemDataSource = new SaleableItemDataSource();

        private readonly ObservableCollection<SaleableItemDataGroup> _groups =
            new ObservableCollection<SaleableItemDataGroup>();

        private readonly StorageFolder _roamingFolder = ApplicationData.Current.RoamingFolder;

        public ObservableCollection<SaleableItemDataGroup> Groups
        {
            get { return _groups; }
        }

        public static async Task<IEnumerable<SaleableItemDataGroup>> GetGroupsAsync()
        {
            await _saleableItemDataSource.GetSaleableItemDataAsync();

            return _saleableItemDataSource.Groups;
        }

        public static async Task<SaleableItemDataGroup> GetGroupAsync(string uniqueId)
        {
            await _saleableItemDataSource.GetSaleableItemDataAsync();
            // Simple linear search is acceptable for small data sets
            IEnumerable<SaleableItemDataGroup> matches =
                _saleableItemDataSource.Groups.Where(group => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<SaleableItemDataItem> GetItemAsync(string uniqueId)
        {
            await _saleableItemDataSource.GetSaleableItemDataAsync();
            // Simple linear search is acceptable for small data sets
            IEnumerable<SaleableItemDataItem> matches =
                _saleableItemDataSource.Groups.SelectMany(group => group.Items).Where(item => item.Id.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        private async Task GetSaleableItemDataAsync()
        {
            _groups.Clear();

            StorageFile customerDataJsonFile = await _roamingFolder.GetFileAsync("SaleableItems.json");
            string jsonText = await FileIO.ReadTextAsync(customerDataJsonFile);
            if (string.IsNullOrWhiteSpace(jsonText)) return;

/*
            var dataUri = new Uri("ms-appx:///DataModel/SaleableItemData.json");
            var file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);
*/

            JsonArray jsonArray = JsonArray.Parse(jsonText);

            var saleableItemGroup = new SaleableItemDataGroup("SaleableItem Default", "Select Items to invoice",
                "SaleableItem SubTitle", "image",
                "SaleableItem Description");
            Groups.Add(saleableItemGroup);

            foreach (JsonValue groupValue in jsonArray)
            {
                JsonObject groupObject = groupValue.GetObject();
                var itemObject = new SaleableItemDataItem(groupObject["Id"].GetString(),
                    groupObject["Name"].GetString(),
                    (Decimal) groupObject["Price"].GetNumber(),
                    groupObject["PictureUrl"].GetString(),
                    groupObject["Active"].GetBoolean());

                saleableItemGroup.Items.Add(itemObject);
            }
        }
    }
}