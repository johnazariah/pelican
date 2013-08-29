// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Pelican.Common;
using Pelican.Data;
using WebApp.Pelican.Api.Models;

namespace Pelican
{
    /// <summary>
    ///     A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class GroupedItemsPage : Page
    {
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();
        private readonly NavigationHelper navigationHelper;
        private ObservableCollection<CustomerDataGroup> _customerDataGroups;

        public GroupedItemsPage()
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
            _customerDataGroups = await CustomerDataSource.GetGroupsAsync();
            DefaultViewModel["Groups"] = _customerDataGroups;

            //var result = await SaleSaveInfo.GetSaleList();
            //pendingInvoiceCount.Text = "Pending Invoices: " + result.Count;
        }

        /// <summary>
        ///     Invoked when a group header is clicked.
        /// </summary>
        /// <param name="sender">The Button used as a group header for the selected group.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        private void Header_Click(object sender, RoutedEventArgs e)
        {
            // Determine what group the Button instance represents
            object group = (sender as FrameworkElement).DataContext;

            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            Frame.Navigate(typeof (GroupDetailPage), ((CustomerDataGroup) group).UniqueId);
        }

        /// <summary>
        ///     Invoked when an item within a group is clicked.
        /// </summary>
        /// <param name="sender">
        ///     The GridView (or ListView when the application is snapped)
        ///     displaying the item clicked.
        /// </param>
        /// <param name="e">Event data that describes the item clicked.</param>
        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            string itemId = ((CustomerDataItem) e.ClickedItem).Id;
            Frame.Navigate(typeof (ItemDetailPage), itemId);
        }

        private async void btnSync_OnClick(object sender, RoutedEventArgs e)
        {
            btnSync.IsEnabled = false;
            CustomerDataGroup firstGroup = _customerDataGroups.FirstOrDefault();
            if (firstGroup != null) firstGroup.Items.Clear();
            var arLiveApi = new HttpClientWebApi();
            await arLiveApi.Refresh();

            PostSales();
            LoadCustomers();

            btnSync.IsEnabled = true;
        }

        private async void PostSales()
        {
            List<Sale> sales = await SaleSaveInfo.GetSaleList();
            var arLiveApi = new HttpClientWebApi();

            foreach (Sale sale in sales)
            {
                sale.Id = Guid.NewGuid().ToString();
                await arLiveApi.PostSaleInvoice(sale);
            }

            SaleSaveInfo.ResetSavedSales();

            //pendingInvoiceCount.Text = "Pending Invoices: " + 0;
        }

        private async void LoadCustomers()
        {
            try
            {
                CustomerDataGroup firstGroup = _customerDataGroups.FirstOrDefault();
                if (firstGroup == null)
                {
                    firstGroup = new CustomerDataGroup("Customer Default", "Customers", "Customer SubTitle", "image",
                        "Customer Description");
                    _customerDataGroups.Add(firstGroup);
                }

                // TODO: Create an appropriate data model for your problem domain to replace the sample data
                ObservableCollection<CustomerDataGroup> customerDataGroups = await CustomerDataSource.GetGroupsAsync();
                if (!customerDataGroups.Any()) return;

                foreach (CustomerDataItem item in customerDataGroups.First().Items)
                {
                    firstGroup.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
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
}