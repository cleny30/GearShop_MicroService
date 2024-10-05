using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using DashboardAdmin.Service;
using System.Threading;
namespace Test_Manager
{
    public class LoginServiceTests
    {
        [Fact]
        public async Task VerifyCredentialsAsync_ReturnsTrue_WhenCredentialsAreValid()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
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

            handlerMock
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

            var httpClient = new HttpClient(handlerMock.Object);
            var loginService = new LoginService(httpClient);

            // Act
            var result = await loginService.VerifyCredentialsAsync("admin", "123");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task VerifyCredentialsAsync_ReturnsFalse_WhenUsernameDoesNotExist()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
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

            var httpClient = new HttpClient(handlerMock.Object);
            var loginService = new LoginService(httpClient);

            // Act
            var result = await loginService.VerifyCredentialsAsync("invalidUsername", "anyPassword");

            // Assert
            Assert.False(result);
        }
    }
}
