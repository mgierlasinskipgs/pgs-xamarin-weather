using Moq;
using Moq.Protected;
using System;
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

        public static void AssertApiWasCalled(Mock<HttpMessageHandler> handlerMock, Uri expectedUri)
        {
            handlerMock.Protected().Verify("SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri == expectedUri),
                ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}
