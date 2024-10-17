using BusinessObject.Models.Entity;
using Dashboard_Admin;
using DashboardAdmin.ImportingProductManagament;
using DashboardAdmin.ProductManagement;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DashboardAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient client;

        public MainWindow()
        {
            client = new HttpClient();
            var contentType =
                new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            InitializeComponent();
            frMain.Content = new Dashboard();
        }

        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = new Dashboard();
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = new ProductPage();
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = new ImportProductPage();
        }

        private void ImportReceipt_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = new ImportReceiptPage();
        }

        private void OrderManagement_Click(object sender, RoutedEventArgs e)
        {
            //frMain.Content = new OrderManagement();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            string FilePath = "RememberMe.json";
            string jsonContent = File.ReadAllText(FilePath);
            var settings = JsonSerializer.Deserialize<Settings>(jsonContent);

            if (settings != null && settings.RememberMe)
            {
            }
            else
            {
                string managerPath = "ManagerUsername.json";
                if (File.Exists(managerPath))
                {
                    File.Delete(managerPath);
                }
            }

            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            string FilePath = "RememberMe.json";
            var settings = new Settings { RememberMe = false };
            string jsonContent = JsonSerializer.Serialize(settings);

            File.WriteAllText(FilePath, jsonContent);

            string managerPath = "ManagerUsername.json";
            if (File.Exists(managerPath))
            {
                File.Delete(managerPath);
            }

            Loginpage loginpage = new Loginpage();
            this.Close();
            loginpage.Show();
        }
        public class Settings
        {
            public bool RememberMe { get; set; }
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
    }
}