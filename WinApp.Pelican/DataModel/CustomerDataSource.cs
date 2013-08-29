using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;

namespace Pelican.Data
{
    public class CustomerDataSource
    {
        private static readonly CustomerDataSource _customerDataSource = new CustomerDataSource();

        private readonly ObservableCollection<CustomerDataGroup> _groups = new ObservableCollection<CustomerDataGroup>();
        private readonly StorageFolder _roamingFolder = ApplicationData.Current.RoamingFolder;

        public ObservableCollection<CustomerDataGroup> Groups
        {
            get { return _groups; }
        }

        public static async Task<ObservableCollection<CustomerDataGroup>> GetGroupsAsync()
        {
            await _customerDataSource.GetCustomerDataAsync();

            return _customerDataSource.Groups;
        }

        public static async Task<CustomerDataGroup> GetGroupAsync(string uniqueId)
        {
            await _customerDataSource.GetCustomerDataAsync();
            // Simple linear search is acceptable for small data sets
            IEnumerable<CustomerDataGroup> matches =
                _customerDataSource.Groups.Where(group => group.UniqueId.Equals(uniqueId));
            return matches.FirstOrDefault();
        }

        public static async Task<CustomerDataItem> GetItemAsync(string uniqueId)
        {
            await _customerDataSource.GetCustomerDataAsync();
            // Simple linear search is acceptable for small data sets
            IEnumerable<CustomerDataItem> matches =
                _customerDataSource.Groups.SelectMany(group => group.Items).Where(item => item.Id.Equals(uniqueId));
            return matches.FirstOrDefault();
        }

        private async Task GetCustomerDataAsync()
        {
            _groups.Clear();

            StorageFile customerDataJsonFile =
                await
                    _roamingFolder.CreateFileAsync(string.Format("{0}.json", "Customers"),
                        CreationCollisionOption.OpenIfExists);
            string jsonText = await FileIO.ReadTextAsync(customerDataJsonFile);
            if (string.IsNullOrWhiteSpace(jsonText)) return;

            JsonArray jsonArray = JsonArray.Parse(jsonText);
            var customerGroup = new CustomerDataGroup("Customer Default", "Customers", "Customer SubTitle", "image",
                "Customer Description");
            Groups.Add(customerGroup);

            foreach (JsonValue groupValue in jsonArray)
            {
                JsonObject groupObject = groupValue.GetObject();
                var itemObject = new CustomerDataItem(groupObject["Id"].GetString(),
                    groupObject["Name"].GetString(),
                    groupObject["Address"].GetString(),
                    groupObject["PictureUrl"].GetString(),
                    (Decimal) groupObject["Balance"].GetNumber());

                customerGroup.Items.Add(itemObject);
            }
        }
    }
}