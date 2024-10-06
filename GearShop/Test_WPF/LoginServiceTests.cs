using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using DashboardAdmin.Service;

namespace Test_Manager
{
    public class LoginServiceTests
    {
        private readonly Mock<HttpMessageHandler> _handlerMock;
        private readonly HttpClient _httpClient;
        private readonly LoginService _loginService;

        public LoginServiceTests()
        {
            _handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(_handlerMock.Object);
            _loginService = new LoginService(_httpClient);
        }

        [Fact]
        public async Task VerifyCredentialsAsync_ReturnsTrue_WhenCredentialsAreValid()
        {
            // Arrange
            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("CheckUsernameExisted")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("true"),
                });

            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("CheckManagerExisted")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("true"),
                });

            // Act
            var result = await _loginService.VerifyCredentialsAsync("admin", "123");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task VerifyCredentialsAsync_ReturnsFalse_WhenUsernameDoesNotExist()
        {
            // Arrange
            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("CheckUsernameExisted")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("false"),
                });

            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("CheckManagerExisted")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("false"),
                });

            var httpClient = new HttpClient(_handlerMock.Object);
            var loginService = new LoginService(httpClient);

            // Act
            var result = await loginService.VerifyCredentialsAsync("invalidUsername", "anyPassword");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task VerifyCredentialsAsync_ReturnsFalse_WhenPasswordIsIncorrect()
        {
            // Arrange
            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("CheckUsernameExisted")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("true"),
                });

            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().Contains("CheckManagerExisted")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("false"),
                });

            // Act
            var result = await _loginService.VerifyCredentialsAsync("admin", "wrongPassword");

            // Assert
            Assert.False(result);
        }
    }
}