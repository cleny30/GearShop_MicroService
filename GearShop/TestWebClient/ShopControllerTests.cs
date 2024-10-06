using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebClient.Controllers;
using WebClient.Service;
using BusinessObject.DTOS;
using BusinessObject.Models.Entity;
using WebClient.Models;

namespace TestWebClient
{
    public class ShopControllerTests
    {
        private readonly Mock<HttpMessageHandler> _handlerMock;
        private readonly HttpClient _httpClient;
        private readonly ShopService _shopService;
        private readonly ShopController _controller;

        public ShopControllerTests()
        {
            _handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(_handlerMock.Object);
            _shopService = new ShopService(); // Khởi tạo ShopService nếu cần
            _controller = new ShopController(_shopService);

            
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithCorrectData()
        {// Giả lập HttpContext và Request
            var context = new DefaultHttpContext();
            context.Request.QueryString = new QueryString("?sort=discount&order=highest&category=1&brand=2&page=1");
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };
            // Arrange
            var products = new List<ProductData> { new ProductData { /* Khởi tạo dữ liệu */ } };
            var brands = new List<Brand> { new Brand { /* Khởi tạo dữ liệu */ } };
            var categories = new List<Category> { new Category { /* Khởi tạo dữ liệu */ } };

            // Giả lập phản hồi cho các cuộc gọi API
            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("GET_ALL_PRODUCTS")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(products)),
                });

            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("GET_ALL_BRANDS")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(brands)),
                });

            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("GET_ALL_CATEGORIES")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(categories)),
                });

            // Act
            var result = await _controller.Index(); // Gọi phương thức Index

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ShopModel>(viewResult.Model); // Kiểm tra kiểu dữ liệu của model
            Assert.NotNull(model);
            // Thêm các kiểm tra khác nếu cần
        }

        [Fact]
        public void GetData_ReturnsCorrectShopModel()
        {
            // Arrange
            var productList = new List<ProductData> { new ProductData { /* Khởi tạo dữ liệu */ } };
            var brandList = new List<Brand> { new Brand { /* Khởi tạo dữ liệu */ } };
            var categoryList = new List<Category> { new Category { /* Khởi tạo dữ liệu */ } };

            // Act
            var result = _shopService.GetData(productList, brandList, categoryList, "", "", "", "", 1); // Gọi phương thức GetData

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ShopModel>(result);
            // Thêm các kiểm tra khác nếu cần
        }
    }
}