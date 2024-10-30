using BusinessObject.DTOS;
using DashboardAdmin.Service;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;

namespace DashboardAdmin.ImportingProductManagament
{
    /// <summary>
    /// Interaction logic for ImportReceiptPage.xaml
    /// </summary>
    public partial class ImportReceiptPage : Page
    {
        private readonly HttpClient client;

        //Load Receipt
        private ObservableCollection<ImportProductModel> receipts { get; set; }
        //------------

        //Paging
        private int itemsPerPage = 9;
        private int currentPage = 1;
        //------

        public ImportReceiptPage()
        {
            client = new HttpClient();
            var contentType =
                new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            DataContext = this;
            InitializeComponent();
            GetDataGrid();
        }

        private async void GetDataGrid()
        {
            var importProductServce = new ImportProductService(client);

            List<ImportProductModel> list = await importProductServce.GetImportProductList();
            receipts = new ObservableCollection<ImportProductModel>(list);
            // Calculate the starting index and number of items for the current page
            int startIndex = (currentPage - 1) * itemsPerPage;
            int count = Math.Min(itemsPerPage, receipts.Count - startIndex);
            ImportReceiptDataGrid.ItemsSource = receipts.Skip(startIndex).Take(count);
            PageCount.Text = currentPage.ToString();
        }

        //Add Button
        private async void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            var importProductService = new ImportProductService(client);

            var button = sender as System.Windows.Controls.Button;
            var dataContext = button.DataContext as ImportProductModel;
            if (dataContext != null)
            {
                var receiptID = dataContext.ImportProductId;
                ImportProductModel model = await importProductService.GetImportProductById(receiptID);
                ImportProductID.Text = receiptID.ToString();
                ImportProductName.Text = model.PersonInCharge;
                ImportProductDate.Text = model.DateImport.ToString();
                TotalMoney.Text = model.Payment.ToString() + "$";
                List<ReceiptProductModel> receiptProductModels = await importProductService.GetReceiptProductById(receiptID);
                ReceiptDataGrid.ItemsSource = receiptProductModels;
            }
        }

        //Went to Next Page
        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if there are more pages
            if ((currentPage * itemsPerPage) < receipts.Count)
            {
                currentPage++;
                PageCount.Text = currentPage.ToString();
                GetDataGrid();
            }
        }

        //Go back to the previous page
        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if there are previous pages
            if (currentPage > 1)
            {
                currentPage--;
                PageCount.Text = currentPage.ToString();
                GetDataGrid();
            }
        }
    }
}
