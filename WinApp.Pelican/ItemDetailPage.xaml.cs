// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using Pelican.Common;
using Pelican.Data;
using WebApp.Pelican.Api.Models;

namespace Pelican
{
    /// <summary>
    ///     A page that displays details for a single item within a group.
    /// </summary>
    public sealed partial class ItemDetailPage : Page
    {
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();
        private readonly NavigationHelper navigationHelper;

        public ItemDetailPage()
        {
            InitializeComponent();
            navigationHelper = new NavigationHelper(this);
            navigationHelper.LoadState += navigationHelper_LoadState;
        }

        /// <summary>
        ///     NavigationHelper is used on each page to aid in navigation and
        ///     process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return navigationHelper; }
        }

        /// <summary>
        ///     This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return defaultViewModel; }
        }

        /// <summary>
        ///     Populates the page with content passed during navigation.  Any saved state is also
        ///     provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        ///     The source of the event; typically <see cref="NavigationHelper" />
        /// </param>
        /// <param name="e">
        ///     Event data that provides both the navigation parameter passed to
        ///     <see cref="Frame.Navigate(Type, Object)" /> when this page was initially requested and
        ///     a dictionary of state preserved by this page during an earlier
        ///     session.  The state will be null the first time a page is visited.
        /// </param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            try
            {
                // TODO: Create an appropriate data model for your problem domain to replace the Customer data
                CustomerDataItem customer = await CustomerDataSource.GetItemAsync((String) e.NavigationParameter);

                IEnumerable<SaleableItemDataGroup> saleableItemDataGroups =
                    await SaleableItemDataSource.GetGroupsAsync();
                DefaultViewModel["ItemsToSell"] = saleableItemDataGroups;

                var currentSale = new Sale();
                currentSale.Customer = customer;
                DefaultViewModel["Sale"] = currentSale;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var sale = DefaultViewModel["Sale"] as Sale;
            if (sale == null) return;
            var soldItem = e.ClickedItem as SaleableItemDataItem;
            if (soldItem == null) return;
            sale.SaleableItems.Add(soldItem);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Record Invoice
            var sale = DefaultViewModel["Sale"] as Sale;
            if (sale == null) return;

            // All Offline Saved Sales
            List<Sale> sales = await SaleSaveInfo.GetSaleList();
            sales.Add(sale);

            SaleSaveInfo.SetSaleList(sales);
            Frame.GoBack();
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the
        /// <see cref="GridCS.Common.NavigationHelper.LoadState" />
        /// and
        /// <see cref="GridCS.Common.NavigationHelper.SaveState" />
        /// .
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }

    public static class SaleSaveInfo
    {
        private static readonly StorageFolder _roamingFolder = ApplicationData.Current.RoamingFolder;

        public static async Task<List<Sale>> GetSaleList()
        {
            string recordedSalesJsonFile = await GetRecordedSalesAsync();

            var sales = new List<Sale>();
            if (!string.IsNullOrWhiteSpace(recordedSalesJsonFile))
            {
                sales = JsonConvert.DeserializeObject<List<Sale>>(recordedSalesJsonFile);
            }
            return sales;
        }

        private static async Task<String> GetRecordedSalesAsync()
        {
            StorageFile saleJsonFile =
                await _roamingFolder.CreateFileAsync("RecordedSales.json", CreationCollisionOption.OpenIfExists);
            string jsonText = await FileIO.ReadTextAsync(saleJsonFile);
            return jsonText;
        }

        public static void SetSaleList(List<Sale> sales)
        {
            string reSerializedSales = JsonConvert.SerializeObject(sales);
            SetRecordedSales(reSerializedSales);
        }

        private static async void SetRecordedSales(String jsonText)
        {
            StorageFile salesDataFile =
                await _roamingFolder.CreateFileAsync("RecordedSales.json", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(salesDataFile, jsonText);
        }

        public static void ResetSavedSales()
        {
            SetRecordedSales(String.Empty);
        }
    }
}