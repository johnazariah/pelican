using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;
using WebApp.Pelican.Api.Models;

namespace Pelican.Data
{
    public class HttpClientWebApi
    {
        private const string ApiEndPoint = "http://pelicanapi.azurewebsites.net/api/{0}";
        private readonly StorageFolder _roamingFolder = ApplicationData.Current.RoamingFolder;

        public async Task Refresh()
        {
            await RefreshCustomers();
            await RefreshSaleableItems();
        }

        private async Task<string> HttpClient(string urlPart)
        {
            string fullUrl = string.Format(ApiEndPoint, urlPart);
            using (var http = new HttpClient())
            {
                HttpResponseMessage response = await http.GetAsync(fullUrl);
                string jsonText = await response.Content.ReadAsStringAsync();
                StorageFile storageFile =
                    await
                        _roamingFolder.CreateFileAsync(string.Format("{0}.json", urlPart),
                            CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(storageFile, jsonText);
                return jsonText;
            }
        }

        private async Task<string> RefreshCustomers()
        {
            string customersJson = await HttpClient("Customers");
            List<CustomerDataItem> customers =
                await JsonConvert.DeserializeObjectAsync<List<CustomerDataItem>>(customersJson);
            string jsonText = await JsonConvert.SerializeObjectAsync(customers.ToArray());
            StorageFile storageFile =
                await
                    _roamingFolder.CreateFileAsync(string.Format("{0}.json", "Customers"),
                        CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(storageFile, jsonText);
            return jsonText;
        }

        private async Task<string> RefreshSaleableItems()
        {
            string saleableItemsJson = await HttpClient("SaleableItems");
            List<SaleableItemDataItem> items =
                await JsonConvert.DeserializeObjectAsync<List<SaleableItemDataItem>>(saleableItemsJson);
            string jsonText = await JsonConvert.SerializeObjectAsync(items.ToArray());
            StorageFile storageFile =
                await
                    _roamingFolder.CreateFileAsync(string.Format("{0}.json", "SaleableItems"),
                        CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(storageFile, jsonText);
            return jsonText;
        }

        public async Task PostSaleInvoice(Sale sale)
        {
            string fullUrl = string.Format(ApiEndPoint, "Sales");
            string data = JsonConvert.SerializeObject(sale);

            using (var http = new HttpClient())
            {
                HttpContent content = new StringContent(data);
                content.Headers.ContentType.MediaType = "application/json";
                HttpResponseMessage response = await http.PutAsync(fullUrl, content);
            }
        }
    }
}