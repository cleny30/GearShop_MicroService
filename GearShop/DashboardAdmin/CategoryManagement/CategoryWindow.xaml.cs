using BusinessObject.DTOS;
using DashboardAdmin.Service;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DashboardAdmin.CategoryManagement
{
    /// <summary>
    /// Interaction logic for CategoryWindow.xaml
    /// </summary>
    public partial class CategoryWindow : Window
    {
        private readonly HttpClient client;
        public CategoryWindow()
        {
            client = new HttpClient();
            var contentType =
                new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            this.DataContext = this;
            InitializeComponent();
            LoadCategory();
        }

        private async void LoadCategory()
        {
            var productPageService = new ProductPageService(client);
            var categories = await productPageService.GetCategoryList();
            foreach (var category in categories)
            {
                var stackPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(10, 0, 0, 0) };

                var editButton = new System.Windows.Controls.Button
                {
                    Margin = new Thickness(0, 0, 5, 0),
                    Background = System.Windows.Media.Brushes.Transparent,
                    BorderBrush = System.Windows.Media.Brushes.Transparent,
                    Padding = new Thickness(0)
                };
                var editIcon = new PackIcon
                {
                    Kind = PackIconKind.Pencil,
                    Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 254, 32))
                };
                editButton.Content = editIcon;
                editButton.Click += EditButton_Click;

                stackPanel.Children.Add(editButton);

                if (category.IsAvailable)
                {
                    var disableButton = new System.Windows.Controls.Button
                    {
                        Background = System.Windows.Media.Brushes.Transparent,
                        BorderBrush = System.Windows.Media.Brushes.Transparent,
                        Padding = new Thickness(0)
                    };
                    var disableIcon = new PackIcon
                    {
                        Kind = PackIconKind.TrashCan,
                        Foreground = System.Windows.Media.Brushes.Red
                    };
                    disableButton.Content = disableIcon;
                    disableButton.Click += DisableButton_Click;

                    stackPanel.Children.Add(disableButton);
                }
                else
                {
                    var disableButton = new System.Windows.Controls.Button
                    {
                        Background = System.Windows.Media.Brushes.Transparent,
                        BorderBrush = System.Windows.Media.Brushes.Transparent,
                        Padding = new Thickness(0)
                    };
                    var disableIcon = new PackIcon
                    {
                        Kind = PackIconKind.LockOpen,
                        Foreground = System.Windows.Media.Brushes.Green
                    };
                    disableButton.Content = disableIcon;
                    disableButton.Click += EnableButton_Click;

                    stackPanel.Children.Add(disableButton);
                }


                category.FunctionContent = stackPanel;
            }
            CategoryDataGrid.ItemsSource = categories;
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Check if the left mouse button was pressed
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                // Initiate the drag-and-drop operation
                this.DragMove();
            }
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var productPageService = new ProductPageService(client);

            var button = sender as System.Windows.Controls.Button;
            var dataContext = button.DataContext as CategoryModel;

            if (dataContext != null)
            {
                var CateID = dataContext.CateId;
                Boolean IsUpdate = true;
                List<CategoryModel> category = await productPageService.GetCategoryList();
                var _category = category.FirstOrDefault(c => c.CateId == CateID);
                CategoryFunc func = new CategoryFunc(IsUpdate, _category);
                func.CategoryFuncClosed += AddCategoryWindow_Closed;
                func.ShowDialog();
            }
        }

        private async void DisableButton_Click(object sender, RoutedEventArgs e)
        {
            var categoryPageService = new CategoryPageService(client);

            var button = sender as System.Windows.Controls.Button;
            var dataContext = button.DataContext as CategoryModel;

            if (dataContext != null)
            {
                var CateID = dataContext.CateId;
                MessageBoxResult result = MessageBox.Show(
        "Do you want to disable the product?",
        "Confirm Disable",
        MessageBoxButton.YesNo,
        MessageBoxImage.Warning);

                // Check the result and act accordingly
                if (result == MessageBoxResult.Yes)
                {
                    if(await categoryPageService.ChangeCategoryStatus(CateID, false))
                    {
                        // Code to delete the product goes here
                        MessageBox.Show("Category With ID: " + CateID + " Disabled Successfully!.");
                        LoadCategory();
                    } else
                    {
                        MessageBox.Show("An error has occured!");
                    }
                }
            }
        }

        private async void EnableButton_Click(object sender, RoutedEventArgs e)
        {
            var categoryPageService = new CategoryPageService(client);

            var button = sender as System.Windows.Controls.Button;
            var dataContext = button.DataContext as CategoryModel;

            if (dataContext != null)
            {
                var CateID = dataContext.CateId;
                MessageBoxResult result = MessageBox.Show(
        "Do you want to enable the product?",
        "Confirm Disable",
        MessageBoxButton.YesNo,
        MessageBoxImage.Warning);

                // Check the result and act accordingly
                if (result == MessageBoxResult.Yes)
                {
                    if (await categoryPageService.ChangeCategoryStatus(CateID, true))
                    {
                        // Code to delete the product goes here
                        MessageBox.Show("Category With ID: " + CateID + " Enabled Successfully!.");
                        LoadCategory();
                    }
                    else
                    {
                        MessageBox.Show("An error has occured!");
                    }
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryFunc func = new CategoryFunc(false, null);
            func.CategoryFuncClosed += AddCategoryWindow_Closed;
            func.ShowDialog();
        }
        private void AddCategoryWindow_Closed(object sender, EventArgs e)
        {
            LoadCategory();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
