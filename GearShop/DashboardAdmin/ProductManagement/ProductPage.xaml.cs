using BusinessObject.DTOS;
using DashboardAdmin.BrandManagement;
using DashboardAdmin.CategoryManagement;
using DashboardAdmin.Service;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;


namespace DashboardAdmin.ProductManagement
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : UserControl
    {
        private readonly HttpClient client;
        private ObservableCollection<ProductModel> products;
        private ObservableCollection<ProductModel> filteredProducts;
        private int itemsPerPage = 7;
        private int currentPage = 1;
        private AddProduct addProductWindow;


        public ProductPage()
        {
            client = new HttpClient();
            var contentType =
                new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            
            DataContext = this;
            InitializeComponent();
            LoadProducts(true);
            InitialSearch();
        }
        //Load Product
        private async void LoadProducts(Boolean IsAvailable)
        {
            var productPageService = new ProductPageService(client);
            List<ProductModel> productList = await productPageService.GetProductList();
            List<ProductModel> availableProducts = productList.Where(p => p.IsAvailable == IsAvailable).ToList();
            // Convert the List to an ObservableCollection
            products = new ObservableCollection<ProductModel>(availableProducts);
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
        //Went to Next Page
        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if there are more pages
            if ((currentPage * itemsPerPage) < filteredProducts.Count)
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

        //Initialize the search function
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

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            Boolean IsUpdate;
            ProductModel product = null;
            List<ProductImageModel> productImages = null;
            List<ProductAttributeModel> productAttributes = null;
            AddProduct addProductWindow = new AddProduct(IsUpdate = false, product, productImages, productAttributes);
            addProductWindow.AddProductWindowClosed += AddProductWindow_AddProductWindowClosed;
            addProductWindow.ShowDialog();
        }

        private void AddProductWindow_AddProductWindowClosed(object sender, EventArgs e)
        {
            Reset();
            LoadProducts(true);
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryWindow cate = new CategoryWindow();
            cate.ShowDialog();
        }

        //Open the Brand Page
        private void BrandButton_Click(object sender, RoutedEventArgs e)
        {
            BrandWindow brand = new BrandWindow();
            brand.ShowDialog();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            InitialSearch();
            Reset();
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (DisableProduct.Content == "Show Enable Products")
            {
                LoadProducts(true);
                DisableProduct.Content = "Show Disable Products";
            }
            else
            {
                LoadProducts(false);
                DisableProduct.Content = "Show Enable Products";
            }
        }

        private async void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            var productPageService = new ProductPageService(client);

            var button = sender as Button;
            var dataContext = button.DataContext as ProductModel;

            if(dataContext != null)
            {
                var ProId = dataContext.ProId;
                Boolean IsUpdate;

                var product = await productPageService.GetProductById(ProId);
                var productImages = await productPageService.GetProductImageById(ProId);
                var productAttributes = await productPageService.GetProductAttributeById(ProId);

                AddProduct addProductWindowUpdate = new AddProduct(IsUpdate = true, product, productImages, productAttributes);
                addProductWindowUpdate.AddProductWindowClosed += AddProductWindow_AddProductWindowClosed;
                addProductWindowUpdate.ShowDialog();
            }
        }

        private void StudentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        public void Reset()
        {
            filteredProducts = new ObservableCollection<ProductModel>(products);
            cbBrand.SelectedValue = -1;
            cbCategory.SelectedValue = -1;
            SearchTextBox.Clear();
            currentPage = 1;
            UpdateDataGrid();
        }
    }
}
