using FluentAssertions.Specialized;
using Moq;
using System.Net;
using System.Net.Http;
using Weather.Api.Clients;

namespace Weather.UnitTests.Assertions
{
    public static class AssertionsExtensions
    {
        public static HttpMessageHandlerAssertions ShouldBe(this Mock<HttpMessageHandler> instance)
        {
            return new HttpMessageHandlerAssertions(instance);
        }

        public static ExceptionAssertions<TException> WithError<TException>(this ExceptionAssertions<TException> exception, 
            HttpStatusCode errorCode, string errorMessage) where TException : ApiException
        {
            return exception.Where(x =>
                x.ErrorCode == (int) errorCode &&
                x.Message == errorMessage, 
                $"exception should have error code: {errorCode} and message: {errorMessage}");
        }
    }
}
