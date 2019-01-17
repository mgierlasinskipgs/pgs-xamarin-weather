using Moq;
using Moq.Protected;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Weather.Api.Clients;

namespace Weather.UnitTests.Concrete
{
    public static class HttpHelper
    {
        public static IApiClient GetApiClientForResponse(HttpResponseMessage response)
        {
            var handlerMock = GetMessageHandlerForResponse(response);
            var httpClient = new HttpClient(handlerMock.Object);
            return new ApiClient(httpClient);
        }

        public static Mock<HttpMessageHandler> GetMessageHandlerForResponse(HttpResponseMessage resposne)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(resposne)
                .Verifiable();

            return handlerMock;
        }
    }
}
