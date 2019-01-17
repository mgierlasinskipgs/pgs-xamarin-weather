using FluentAssertions;
using FluentAssertions.Primitives;
using Moq;
using Moq.Protected;
using System;
using System.Net.Http;
using System.Threading;

namespace Weather.UnitTests.Assertions
{
    public class HttpMessageHandlerAssertions : ReferenceTypeAssertions<Mock<HttpMessageHandler>, HttpMessageHandlerAssertions>
    {
        protected override string Identifier => "HttpMessageHandlerAssertions";

        public HttpMessageHandlerAssertions(Mock<HttpMessageHandler> instance)
        {
            Subject = instance;
        }

        public AndConstraint<HttpMessageHandlerAssertions> CalledOnce(Uri expectedUri)
        {
            Subject.Protected().Verify("SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get &&
                    req.RequestUri == expectedUri),
                ItExpr.IsAny<CancellationToken>());

            return new AndConstraint<HttpMessageHandlerAssertions>(this);
        }
    }
}
