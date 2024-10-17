using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using DashboardAdmin;
using DashboardAdmin.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Dashboard_Admin
{
    /// <summary>
    /// Interaction logic for Loginpage.xaml
    /// </summary>
    public partial class Loginpage : Window
    {


        string filePath = "RememberMe.json";
        string managerPath = "ManagerUsername.json";
        private readonly HttpClient client;
        
        public Loginpage()
        {
            client = new HttpClient();
            var contentType =
                new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            InitializeComponent();
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

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginService = new LoginService(client);

            Overlay.Visibility = Visibility.Visible;
            bool? isChecked = RememberMeCheckbox.IsChecked;
            if (await VerificationAsync())
            {
                MessageBox.Show("Login Successfully");
                if (isChecked == true)
                {
                    if (!File.Exists(filePath))
                    {
                        // Create a simple JSON object as an s
                        string jsonContent = "{\"RememberMe\": true}";

                        // Write the JSON content to the file
                        File.WriteAllText(filePath, jsonContent);
                    }
                    else
                    {
                        var settings = new Settings { RememberMe = true };
                        string jsonContent = JsonSerializer.Serialize(settings);

                        File.WriteAllText(filePath, jsonContent);
                    }
                }

                if (!File.Exists(managerPath))
                {
                    ManagerModel model = await loginService.GetManagerByUsername(txtUsername.Text);

                    // Create a simple JSON object
                    var jsonObject = new { Username = model.Fullname};

                    // Serialize the object to a JSON string
                    string jsonContent = JsonSerializer.Serialize(jsonObject);

                    // Write the JSON content to the file
                    File.WriteAllText(managerPath, jsonContent);
                }


                Overlay.Visibility = Visibility.Collapsed;
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            } else
            {
                Overlay.Visibility = Visibility.Collapsed;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();

        private async Task<bool> VerificationAsync()
        {
            // Khởi tạo HttpClient và LoginService
            var client = new HttpClient();
            var loginService = new LoginService(client);

            // Xóa thông báo lỗi trước đó
            errorUsername.Text = "";
            errorPassword.Text = "";

            // Kiểm tra xem tên người dùng và mật khẩu có được nhập không
            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                errorUsername.Text = "Please enter username";
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Password.Trim()))
            {
                errorPassword.Text = "Please enter password";
                return false;
            }

            // Kiểm tra xem tài khoản có tồn tại không
            bool usernameExists = await loginService.CheckUsernameExistsAsync(txtUsername.Text);
            if (!usernameExists)
            {
                errorUsername.Text = "Username does not exist";
                return false;
            }

            // Gọi phương thức VerifyCredentialsAsync từ LoginService
            bool isValid = await loginService.VerifyCredentialsAsync(txtUsername.Text, txtPassword.Password);

            if (!isValid)
            {
                errorPassword.Text = "Username or Password does not exist";
            }

            return isValid;
        }

        public class Settings
        {
            public bool RememberMe { get; set; }
        }

    }
}
