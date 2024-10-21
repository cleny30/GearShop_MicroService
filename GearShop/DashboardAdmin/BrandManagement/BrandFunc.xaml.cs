using BusinessObject.DTOS;
using DashboardAdmin.Service;
using Microsoft.Win32;
using Repository.Core.Cloudiary;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;


namespace DashboardAdmin.BrandManagement
{
    /// <summary>
    /// Interaction logic for BrandFunc.xaml
    /// </summary>
    public partial class BrandFunc : Window
    {
        private readonly HttpClient client;
        public ObservableCollection<string> SelectedBrandLogo { get; set; }

        //Used for updating
        public Boolean _IsUpdate { get; set; }
        public BrandModel _BrandModel { get; set; }
        public string changedBrandLogo { get; set; }
        //-----------------
        public event EventHandler BrandFuncClosed;

        public BrandFunc(bool IsUpdate, BrandModel brand)
        {
            client = new HttpClient();
            var contentType =
                new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            _IsUpdate = IsUpdate;
            _BrandModel = brand;
            SelectedBrandLogo = new ObservableCollection<string>();
            DataContext = this;
            InitializeComponent();
            if (IsUpdate)
            {
                InputDataForUpdating();
            }
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
        private void SelectFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;  // Ensure only one file can be selected
            if (openFileDialog.ShowDialog() == true)
            {
                SelectedBrandLogo.Clear();  // Optionally clear previous selections
                SelectedBrandLogo.Add(openFileDialog.FileName);  // Add the selected file
            }
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            var brandPageService = new BrandPageService(client);

            CloudinaryManagement cloud = new CloudinaryManagement();
            // Show the overlay
            Overlay.Visibility = Visibility.Visible;
            // Disable the main window
            this.IsEnabled = false;
            try
            {
                if (Validation())
                {
                    if (!_IsUpdate)
                    {
                       if(await brandPageService.InsertNewBrand(SelectedBrandLogo[0], txtBrandName.Text))
                       {
                            MessageBox.Show("Insert brand with name: " + txtBrandName.Text + " Successfully!");
                            this.Close();
                            BrandFuncClosed?.Invoke(this, EventArgs.Empty);
                       } else
                       {
                            MessageBox.Show("Something went wrong when inserting brand!");
                       }
                    }
                    else
                    {
                        if (await brandPageService.UpdateBrand(SelectedBrandLogo[0], txtBrandName.Text, changedBrandLogo, _BrandModel.BrandId))
                        {
                            MessageBox.Show("Update brand with ID: " + _BrandModel.BrandId + " Successfully!");
                            this.Close();
                            BrandFuncClosed?.Invoke(this, EventArgs.Empty);
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong when inserting brand!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Something went wrong! Check the error for more information");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Hide the overlay and enable the window again
                Overlay.Visibility = Visibility.Collapsed;
                this.IsEnabled = true;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();

        private void InputDataForUpdating()
        {
            txtBrandName.Text = _BrandModel.BrandName;
            SelectedBrandLogo.Add(_BrandModel.BrandLogo);
            changedBrandLogo = _BrandModel.BrandLogo;
        }

        private bool Validation()
        {
            bool allCheck = true;
            errorBrandName.Text = "";
            errorBrandImage.Text = "";

            //Brand Name
            if (txtBrandName.Text == "")
            {
                errorBrandName.Text = "Please insert Name!";
            }

            //Brand Image
            bool containsInvalidExtension = false;
            if (SelectedBrandLogo == null || SelectedBrandLogo.Count == 0)
            {
                errorBrandImage.Text = "Please select images";
                allCheck = false;
            }
            else
            {
                foreach (string file in SelectedBrandLogo)
                {
                    string extension = System.IO.Path.GetExtension(file);
                    if (extension != ".png" && extension != ".jpg" && extension != ".webp")
                    {
                        containsInvalidExtension = true;
                        break;
                    }
                }

                if (containsInvalidExtension)
                {
                    errorBrandImage.Text = "Please select only .png, .jpg and .webp files";
                    allCheck = false;
                }
            }
            return allCheck;
        }
    }
}
