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
    /// Interaction logic for ImportProductPage.xaml
    /// </summary>
    public partial class ImportProductPage : Page
    {
        private readonly HttpClient client;
        //Load Product
        private ObservableCollection<ProductModel> products { get; set; }
        private ObservableCollection<ProductModel> filteredProducts { get; set; }
        public ObservableCollection<ProductModel>? CartProducts { get; set; }
        bool isOutOfStock = true;
        //------------
        //Paging
        private int itemsPerPage = 8;
        private int currentPage = 1;
        //------
        public ImportProductPage()
        {
            client = new HttpClient();
            var contentType =
                new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            DataContext = this;
            InitializeComponent();
            LoadProducts();
            InitialSearch();
            CartImport();
        }

        //Import Cart
        public void CartImport()
        {
            CartProducts = new ObservableCollection<ProductModel>();
            Cart.ItemsSource = CartProducts;
        }

        //Change Product List
        private void ToggleContentButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Content.ToString() == "Show Normal Product")
                {
                    button.Content = "Show Out of Stocks";
                    titleChange.Text = "PRODUCTS";
                    isOutOfStock = false;
                    LoadProducts();
                    Reset();
                }
                else
                {
                    button.Content = "Show Normal Product";
                    titleChange.Text = "OUT OF STOCKS";
                    isOutOfStock = true;
                    LoadProducts();
                    Reset();
                }
            }
        }

        //Load Product into Grid
        private async void LoadProducts()
        {
            var productPageService = new ProductPageService(client);
            List<ProductModel> productList = await productPageService.GetProductList();
            if (isOutOfStock)
            {
                productList = productList.Where(p => p.ProQuan <= 0).ToList();
            }
            else
            {
                productList = productList.Where(p => p.ProQuan > 0).ToList();
            }
            // Convert the List to an ObservableCollection
            products = new ObservableCollection<ProductModel>(productList);
            // Initially, filteredStudents is the same as students
            filteredProducts = new ObservableCollection<ProductModel>(products);
            // Display data for the current page
            UpdateDataGrid();
            PageCount.Text = currentPage.ToString();
        }

        //Update datagrid when using search
        private void UpdateDataGrid()
        {
            // Calculate the starting index and number of items for the current page
            int startIndex = (currentPage - 1) * itemsPerPage;
            int count = Math.Min(itemsPerPage, filteredProducts.Count - startIndex);
            // Update the data grid with the items for the current page
            ProductDataGrid.ItemsSource = filteredProducts.Skip(startIndex).Take(count);
        }

        //Go to next page
        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if there are more pages
            if ((currentPage * itemsPerPage) < products.Count)
            {
                currentPage++;
                PageCount.Text = currentPage.ToString();
                UpdateDataGrid();
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
                UpdateDataGrid();
            }
        }

        //Search The grid
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Filter the students based on the search text
            string searchText = SearchTextBox.Text.ToLower();
            int? BrandID = cbBrand.SelectedValue as int?;
            int? CateID = cbCategory.SelectedValue as int?;
            filteredProducts = new ObservableCollection<ProductModel>(products.Where(s => s.ProName.ToLower().Contains(searchText) && (s.BrandId == BrandID || !BrandID.HasValue || BrandID == -1)
                                                                      && (s.CateId == CateID || !CateID.HasValue || CateID == -1)));
            // Reset to the first page after a search
            currentPage = 1;
            PageCount.Text = currentPage.ToString();
            UpdateDataGrid();
        }

        //Reset the search and grid
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            InitialSearch();
            Reset();
        }

        private async void InitialSearch()
        {
            var productPageService = new ProductPageService(client);

            List<BrandModel> brands = await productPageService.GetBrandList();
            brands.Insert(0, new BrandModel { BrandId = -1, BrandName = "All" });
            cbBrand.ItemsSource = brands;
            cbBrand.DisplayMemberPath = "BrandName";
            cbBrand.SelectedValuePath = "BrandId";
            cbBrand.SelectedValue = -1;

            List<CategoryModel> categorys = await productPageService.GetCategoryList();
            categorys.Insert(0, new CategoryModel { CateId = -1, CateName = "All" });
            cbCategory.ItemsSource = categorys;
            cbCategory.DisplayMemberPath = "CateName";
            cbCategory.SelectedValuePath = "CateId";
            cbCategory.SelectedValue = -1;
        }

        //Reset
        public void Reset()
        {
            filteredProducts = new ObservableCollection<ProductModel>(products);
            cbBrand.SelectedValue = -1;
            cbCategory.SelectedValue = -1;
            SearchTextBox.Clear();
            currentPage = 1;
            UpdateDataGrid();
        }

        //Add Product to Cart
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the current row's data context (which is the bound data item)
            var button = sender as System.Windows.Controls.Button;
            var dataContext = button.DataContext as ProductModel;
            if (dataContext != null)
            {
                // Retrieve the ID of the product
                var productId = dataContext.ProId;
                var productName = dataContext.ProName;
                var productPrice = dataContext.ProPrice;
                var productQuantity = dataContext.ProQuan;

                var existingProduct = CartProducts.SingleOrDefault(p => p.ProId == productId);

                if (existingProduct != null)
                {
                    // Update the quantity of the existing product
                    existingProduct.ProQuan += 1;
                    Cart.ItemsSource = CartProducts;
                    GetTotalMoney();
                }
                else
                {
                    ProductModel product = new ProductModel
                    {
                        ProId = productId,
                        ProName = productName,
                        ProPrice = productPrice,
                        ProQuan = productQuantity,
                    };
                    ImportProductChooser importChooser = new ImportProductChooser(product, CartProducts, false);
                    bool? result = importChooser.ShowDialog();
                    GetTotalMoney();
                }

            }
        }

        //Get Total Money
        public void GetTotalMoney()
        {
            TotalMoney.Text = CartProducts.Sum(p => p.TotalPrice).ToString() + "$";
        }

        //Submit the cart
        private async void Submit_Button(object sender, RoutedEventArgs e)
        {
            // Show the overlay
            if (CartProducts.Count != 0)
            {
                Overlay.Visibility = Visibility.Visible;
                var importProductService = new ImportProductService(client);

                bool isSuccess = await importProductService.ImportProduct(Cart, CartProducts);
                if (isSuccess)
                {
                    LoadProducts();
                    // Hide the overlay and enable the window again
                    Overlay.Visibility = Visibility.Collapsed;
                    this.IsEnabled = true;
                    CartProducts.Clear();
                    MessageBox.Show("Import Successfully");
                }
                else
                {
                    MessageBox.Show("An unexpected errror has occured!");
                    // Hide the overlay and enable the window again
                    Overlay.Visibility = Visibility.Collapsed;
                    this.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("There are no product in cart!");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var dataContext = button.DataContext as ProductModel;
            if (dataContext != null)
            {
                var productId = dataContext.ProId;
                var productName = dataContext.ProName;
                var productPrice = dataContext.ProPrice;
                var productQuantity = dataContext.ProQuan;

                ProductModel product = new ProductModel
                {
                    ProId = productId,
                    ProName = productName,
                    ProPrice = productPrice,
                    ProQuan = productQuantity,
                };

                ImportProductChooser importChooser = new ImportProductChooser(product, CartProducts, true);
                bool? result = importChooser.ShowDialog();
                GetTotalMoney();
            }
        }

        //Delete product from cart
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the current row's data context (which is the bound data item)
            var button = sender as Button;
            var dataContext = button.DataContext as ProductModel; // Assuming your data item class is named ProductModel
            if (dataContext != null)
            {
                // Remove the product from the CartProducts collection
                CartProducts.Remove(dataContext);
                GetTotalMoney();
            }
        }
    }
}
