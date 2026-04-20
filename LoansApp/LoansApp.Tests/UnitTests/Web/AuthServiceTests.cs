using Blazored.LocalStorage;
using LoansApp.Web.Services;
using Moq.Protected;
using System.Net;

namespace LoansApp.Tests.UnitTests.Web
{
    public class AuthServiceTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<ILocalStorageService> _localStorageMock;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _localStorageMock = new Mock<ILocalStorageService>();
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://localhost:7253/")
            };

            _httpClientFactoryMock
                .Setup(f => f.CreateClient("Api"))
                .Returns(_httpClient);

            _authService = new AuthService(_httpClientFactoryMock.Object, _localStorageMock.Object);
        }

        [Fact]
        public async Task Login_Successful_ReturnsTrue_And_SavesToken()
        {
            // Arrange
            var email = "test@example.com";
            var password = "Password123";
            var fakeToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.validtoken123";

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent($"\"{fakeToken}\"")
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _authService.Login(email, password);

            // Assert
            result.Should().BeTrue();

            _localStorageMock.Verify(x => x.SetItemAsync<string>(
                "authToken",
                It.IsAny<string>(),
                It.IsAny<CancellationToken?>()),
                Times.Once());
        }

        [Fact]
        public async Task Login_Failed_ReturnsFalse()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _authService.Login("bad@email.com", "wrongpass");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetToken_ReturnsTrimmedToken_WhenTokenExists()
        {
            // Arrange
            const string storedToken = "   \"my.jwt.token.here\"   ";

            _localStorageMock
                .Setup(l => l.GetItemAsync<string>("authToken", It.IsAny<CancellationToken?>()))
                .ReturnsAsync(storedToken);

            // Act
            var result = await _authService.GetToken();

            // Assert
            result.Should().NotBeNull();
            result.Should().Be("my.jwt.token.here");
        }

        [Fact]
        public async Task GetToken_ReturnsNull_WhenNoTokenStored()
        {
            // Arrange
            _localStorageMock
                .Setup(l => l.GetItemAsync<string>("authToken", It.IsAny<CancellationToken>()))
                .ReturnsAsync((string?)null);

            // Act
            var token = await _authService.GetToken();

            // Assert
            token.Should().BeNull();
        }

        [Fact]
        public async Task ClearToken_CallsRemoveItemAsync()
        {
            // Act
            await _authService.ClearToken();

            // Assert
            _localStorageMock.Verify(l => l.RemoveItemAsync(
                "authToken",
                It.IsAny<CancellationToken?>()),  
                Times.Once());
        }

        [Fact]
        public async Task Login_Exception_ReturnsFalse()
        {
            // Arrange
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Network error"));

            // Act
            var result = await _authService.Login("test@test.com", "password");

            // Assert
            result.Should().BeFalse();
        }
    }
}
