using BusinessObject.DTOS;
using Microsoft.Extensions.Options;
using NuGet.Protocol.Core.Types;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.Json;
using WebClient.APIEndPoint;
using WebClient.Models;

namespace WebClient.Service
{
    public class OrderService
    {
        private readonly HttpClient client = null;
        private readonly EmailSettings _emailSettings;

        public OrderService(IOptions<EmailSettings> emailSettings)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> Checkout(OrderModel orderModel)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string urlCartData = string.Format(ApiEndPoints_Cart.GET_CHECKED_PRODUCTS, orderModel.Username, orderModel.proId);

            HttpResponseMessage response = await client.GetAsync(urlCartData);
            string strCartData = await response.Content.ReadAsStringAsync();
            var userCartData = JsonSerializer.Deserialize<List<UserCartData>>(strCartData, options);

            if (userCartData != null && userCartData.Any())
            {
                foreach (var item in userCartData)
                {
                    if (item.Product.ProQuan <= 0 || item.Product.ProQuan < item.CartModel.Quantity)
                    {
                        return false;
                    }
                }
                var resId = await client.GetAsync(ApiEndPoints_Order.GET_NEW_ORDERID);
                string orderId = await resId.Content.ReadAsStringAsync();
                orderId = JsonSerializer.Deserialize<string>(orderId, options);
                //----------------------------------------
                string resxFilePath = "WebClient.Resource.Template";

                ResourceManager resourceManager = new ResourceManager(resxFilePath, Assembly.GetExecutingAssembly());
                string tr_tag = resourceManager.GetString("tr_tag");
                int index = 1;
                string table_content = "";

                List<OrderDetailModel> orderDetail = new List<OrderDetailModel>();
                List<ProductData> products = new List<ProductData>();
                foreach (var cartItem in userCartData)
                {
                    var product = userCartData.FirstOrDefault(p => p.Product.ProId == cartItem.Product.ProId).Product;
                    var updateQuantity = product.ProQuan - cartItem.CartModel.Quantity;
                    product.ProQuan = updateQuantity;
                    products.Add(product);

                    string tmp = tr_tag;

                    string formattedIndex = index.ToString("D2");

                    tmp = tmp.Replace("@param01", product.ProName);
                    tmp = tmp.Replace("@param02", cartItem.CartModel.Quantity.ToString());
                    tmp = tmp.Replace("@param03", cartItem.CartModel.ProPrice.ToString());

                    table_content += tmp;
                    index++;
                    //-----------------------------------------------------------------
                    OrderDetailModel orderDetailModel = new OrderDetailModel
                    {
                        OrderId = orderId,
                        ProId = cartItem.Product.ProId,
                        ProName = cartItem.Product.ProName,
                        Quantity = cartItem.CartModel.Quantity,
                        Price = cartItem.CartModel.ProPrice
                    };
                    orderDetail.Add(orderDetailModel);
                }
                var resAccount = await client.GetAsync($"{ApiEndpoints_Customer.GET_CUSTOMER_BY_USERNAME}/{orderModel.Username}");
                string strCustomer = await resAccount.Content.ReadAsStringAsync();
                CustomerModel customer = JsonSerializer.Deserialize<CustomerModel>(strCustomer, options);

                orderModel.OrderId = orderId;
                orderModel.EndDate = null; // time will be set when order comes to the customer
                orderModel.Email = customer.Email;
                string jsonProduct = JsonSerializer.Serialize(products, options);
                var contentProduct = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PutAsync(ApiEndpoints_Product.UPDATE_QUANTITY_PRODUCT, contentProduct);

                string jsonOrder = JsonSerializer.Serialize(orderModel, options);
                var contentOrder = new StringContent(jsonOrder, Encoding.UTF8, "application/json");
                await client.PostAsync(ApiEndPoints_Order.ADD_ORDER, contentOrder);

                string jsonOrderDetails = JsonSerializer.Serialize(orderDetail, options);
                var contentOrderDetails= new StringContent(jsonOrderDetails, Encoding.UTF8, "application/json");
                await client.PostAsync(ApiEndPoints_Order.ADD_ORDER_DETAILS, contentOrderDetails);

                var url = string.Format(ApiEndPoints_Cart.DELETE_CART, orderModel.proId, orderModel.Username);
                await client.DeleteAsync(url);

              
                // Send email asynchronously
                _ = Task.Run(() => Invoice(orderModel, table_content, customer));

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Invoice(OrderModel orderModel, string table_content, CustomerModel account)
        {
            try
            {
                string fromEmail = "clenynguyen@gmail.com";
                string password = "pyaotxulqjcgttwl";

                string reciever = account.Email;

                string resxFilePath = "WebClient.Resource.Template";

                ResourceManager resourceManager = new ResourceManager(resxFilePath, Assembly.GetExecutingAssembly());

                string htmlContent = resourceManager.GetString("invoice");

                htmlContent = htmlContent.Replace("@param01", orderModel.OrderId);
                htmlContent = htmlContent.Replace("@param02", orderModel.StartDate.ToString());
                htmlContent = htmlContent.Replace("@param03", orderModel.TotalPrice.ToString());
                htmlContent = htmlContent.Replace("@param04", orderModel.Fullname);
                htmlContent = htmlContent.Replace("@param05", orderModel.Phone);
                htmlContent = htmlContent.Replace("@param06", orderModel.Address);

                htmlContent = htmlContent.Replace("@param07", table_content);

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromEmail);
                message.Subject = "Invoice";
                message.To.Add(new MailAddress(reciever));
                message.Body = htmlContent;
                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromEmail, password),
                    EnableSsl = true,
                };
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

    }
}
