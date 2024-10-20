using BusinessObject.DTOS;
using DashboardAdmin.Service;
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
    /// Interaction logic for CategoryFunc.xaml
    /// </summary>
    public partial class CategoryFunc : Window
    {
        private readonly HttpClient client;

        //Used for Updating
        public Boolean _IsUpdate { get; set; }
        public CategoryModel _CategoryModel { get; set; }
        //-----------------

        public event EventHandler CategoryFuncClosed;
        public CategoryFunc(bool IsUpdate, CategoryModel _cate)
        {
            client = new HttpClient();
            var contentType =
                new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            _IsUpdate = IsUpdate;
            _CategoryModel = _cate;

            this.DataContext = this;
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

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            var categoryPageService = new CategoryPageService(client);

            if (await Validation())
            {
                if (!_IsUpdate)
                {
                    if(await categoryPageService.InsertNewCategory(txtCategoryName.Text, txtCategoryKeyword.Text))
                    {
                        MessageBox.Show("Insert Category With Name: " + txtCategoryName.Text + " Successfully!");
                        this.Close();
                        CategoryFuncClosed?.Invoke(this, EventArgs.Empty);
                    } else
                    {
                        MessageBox.Show("Something went wrong when inserting category!");
                    }
                }
                else
                {
                    if (await categoryPageService.UpdateCategory(_CategoryModel.CateId, txtCategoryName.Text))
                    {
                        MessageBox.Show("Update Category With ID: " + _CategoryModel.CateId + " Successfully!");
                        this.Close();
                        CategoryFuncClosed?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong when updating category!");
                    }
                }

            }
            else
            {
                MessageBox.Show("Something went wrong! Check the error for more information");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();

        private void InputDataForUpdating()
        {
            txtCategoryName.Text = _CategoryModel.CateName;
            txtCategoryKeyword.IsEnabled = false;
            txtCategoryKeyword.Text = _CategoryModel.Keyword;
        }

        private async Task<Boolean> Validation()
        {
            var categoryPageService = new CategoryPageService(client);

            bool allCheck = true;
            bool IsKeywordExisted = await categoryPageService.IsKeywordExisted(txtCategoryKeyword.Text);

            errorCateName.Text = "";
            errorCateKeyword.Text = "";

            if (!_IsUpdate)
            {
                if (txtCategoryName.Text == "")
                {
                    allCheck = false;
                    errorCateName.Text = "Name is empty!";
                }

                if (txtCategoryKeyword.Text == "")
                {
                    errorCateKeyword.Text = "Keyword is empty!";
                    allCheck = false;
                }
                else if (IsKeywordExisted)
                {
                    allCheck = false;
                    errorCateKeyword.Text = "Keyword Already Existed!";
                }
                else if (txtCategoryKeyword.Text.Length == 0 || txtCategoryKeyword.Text.Length > 2)
                {
                    allCheck = false;
                    errorCateKeyword.Text = "Keyword Length can't be longer than 2!";
                }
            }
            return allCheck;
        }
    }
}
