﻿using BusinessObject.DTOS;
using DashboardAdmin.Service;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DashboardAdmin.OrderManagement
{

    public partial class OrderInfo : Window
    {
        private readonly HttpClient client;
        public OrderModel _orderModel { get; set; }
        public List<OrderDetailModel> _orderDetailModels { get; set; }
        public event EventHandler OrderInfoClosed;
        public OrderInfo(OrderModel order, List<OrderDetailModel> orderDetailModels)
        {
            client = new HttpClient();
            var contentType =
                new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            _orderModel = order;
            _orderDetailModels = orderDetailModels;
            InitializeComponent();
            GetOrder();
            GetButton();
        }

        private void GetButton()
        {
            switch (_orderModel.Status)
            {
                case 1:
                    CreateAcceptButton("Accept Order");
                    CreateCancelButton();
                    break;

                case 2:
                    CreateAcceptButton("Ship Order");
                    break;

                case 3:
                    CreateAcceptButton("Complete Order");
                    break;

                case 4:
                    break;
            }
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Check if the left mouse button was pressed
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Initiate the drag-and-drop operation
                this.DragMove();
            }
        }
        private void GetOrder()
        {
            txtOrderID.Text = _orderModel.OrderId;
            txtCreateDate.Text = _orderModel.StartDate.ToString();
            if (_orderModel.EndDate != null)
            {
                txtShippingDate.Text = _orderModel.EndDate.ToString();
            }
            else
            {
                txtShippingDate.Text = "To Be Decided";
            }
            txtFullname.Text = _orderModel.Fullname;
            txtPhoneNumber.Text = _orderModel.Phone;
            txtEmail.Text = _orderModel.Email;
            txtAddress.Text = _orderModel.Address;
            txtDescription.Text = _orderModel.OrderDes;
            TotalPrice.Text = _orderModel.TotalPrice.ToString() + "$";
            OrderDetailDataGrid.ItemsSource = _orderDetailModels;
        }

        private void CreateAcceptButton(string Content)
        {
            // Create a new button
            Button acceptButton = new Button();
            // Set button properties
            acceptButton.Content = Content;
            acceptButton.Width = 300;
            acceptButton.Margin = new Thickness(0, 10, 0, 10);
            acceptButton.HorizontalAlignment = HorizontalAlignment.Center;
            // Set the Grid.Column property
            Grid.SetColumn(acceptButton, 1);
            // Add the button to the parent container (Grid)
            acceptButton.Click += AcceptButton_Click;
            buttonField.Children.Add(acceptButton);
        }

        private async void AcceptButton_Click(object sender, RoutedEventArgs e)
        {

            var orderManagementService = new OrderManagementService(client);

            if (await orderManagementService.ChangeOrderStatus(_orderModel.OrderId, _orderModel.Status + 1))
            {
                MessageBox.Show($"Status of Order ID {_orderModel.OrderId} has been changed from {Status(_orderModel.Status)} to {Status(_orderModel.Status + 1)}");
                this.Close();
                OrderInfoClosed?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show($"Error in processing");
            }
        }
        public string Status(int Status)
        {
            string _status = "";
            switch (Status)
            {
                case 1:
                    _status = "Pending";
                    break;
                case 2:
                    _status = "Accepted";
                    break;

                case 3:
                    _status = "Shipping";
                    break;

                case 4:
                    _status = "Completed";
                    break;
            }
            return _status;
        }

        private void CreateCancelButton()
        {
            // Create a new button
            Button cancelButton = new Button();
            // Set button properties
            cancelButton.Content = "Cancel Order";
            cancelButton.Width = 300;
            cancelButton.HorizontalAlignment = HorizontalAlignment.Center;
            // Apply the style from resources
            cancelButton.Style = (Style)FindResource("MaterialDesignPaperButton");
            // Set the foreground color
            cancelButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF673AB7"));
            // Set the Grid.Column property
            Grid.SetColumn(cancelButton, 1);
            // Add the button to the parent container (Grid)
            cancelButton.Click += CancelButton_Click;
            buttonField.Children.Add(cancelButton);
        }
        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var orderManagementService = new OrderManagementService(client);

            if (await orderManagementService.ChangeOrderStatus(_orderModel.OrderId, 0))
            {
                MessageBox.Show($"Status of Order ID {_orderModel.OrderId} has been Cancelled");
                this.Close();
                OrderInfoClosed?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show($"Error in processing");
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
