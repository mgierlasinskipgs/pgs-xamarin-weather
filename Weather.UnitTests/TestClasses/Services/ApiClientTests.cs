using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Api.Clients;
using Weather.Api.Models;
using Weather.Api.Resources;
using Xunit;
using static Weather.UnitTests.Concrete.HttpHelper;

namespace Weather.UnitTests.TestClasses.Services
{
    public class ApiClientTests
    {
        [Theory]
        [InlineData("London", "imperial", "123")]
        [InlineData("Norwich", "metric", "456")]
        public async Task Api_should_be_called_once_with_proper_url_and_parameters(string query, string units, string apiKey)
        {
            // Arrange
            var fixture = new Fixture();
            var handlerMock = GetMessageHandlerForResponse(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(fixture.Create<CurrentWeather>()))
            });

            var httpClient = new HttpClient(handlerMock.Object);
            var apiClient = new ApiClient(httpClient)
            {
                BaseUrl = "https://test.com/api/weather",
                ApiKey = apiKey
            };

            // Act
            var result = await apiClient.GetCurrentWeather(query, units);

            // Assert
            AssertApiWasCalled(handlerMock, new Uri($"https://test.com/api/weather?q={query}&units={units}&APPID={apiKey}"));
        }

        [Theory, AutoData]
        public async Task Calling_api_with_success_status_code_should_return_valid_dto_objects(CurrentWeather weatherData)
        {
            // Arrange
            var handlerMock = GetMessageHandlerForResponse(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(weatherData))
            });

            var httpClient = new HttpClient(handlerMock.Object);
            var apiClient = new ApiClient(httpClient)
            {
                BaseUrl = "https://test.com/api/weather",
                ApiKey = "123"
            };

            // Act
            var result = await apiClient.GetCurrentWeather("London", "imperial");

            // Assert
            result.Should().BeEquivalentTo(weatherData);

            AssertApiWasCalled(handlerMock, new Uri("https://test.com/api/weather?q=London&units=imperial&APPID=123"));
        }

        [Fact]
        public void Calling_api_with_not_found_status_code_should_throw_exception()
        {
            // Arrange
            var handlerMock = GetMessageHandlerForResponse(new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound });

            var httpClient = new HttpClient(handlerMock.Object);
            var apiClient = new ApiClient(httpClient)
            {
                BaseUrl = "https://test.com/api/weather",
                ApiKey = "123"
            };
            
            // Act
            Func<Task> act = async () =>
            {
                var result = await apiClient.GetCurrentWeather("InvalidCity", "metric");
            };

            // Assert
            act.Should().Throw<ApiException>().Where(x => 
                x.ErrorCode == (int)HttpStatusCode.NotFound && 
                x.Message == string.Format(ErrorMessages.NotFound, "InvalidCity"));

            AssertApiWasCalled(handlerMock, new Uri("https://test.com/api/weather?q=InvalidCity&units=metric&APPID=123"));
        }

        [Fact]
        public void Calling_api_with_no_content_should_throw_exception()
        {
            // Arrange
            var handlerMock = GetMessageHandlerForResponse(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

            var httpClient = new HttpClient(handlerMock.Object);
            var apiClient = new ApiClient(httpClient)
            {
                BaseUrl = "https://test.com/api/weather",
                ApiKey = "123"
            };

            // Act
            Func<Task> act = async () =>
            {
                var result = await apiClient.GetCurrentWeather("London", "metric");
            };

            // Assert
            act.Should().Throw<ApiException>().Where(x =>
                x.ErrorCode == (int)HttpStatusCode.NoContent &&
                x.Message == ErrorMessages.NoContent);

            AssertApiWasCalled(handlerMock, new Uri("https://test.com/api/weather?q=London&units=metric&APPID=123"));
        }
    }
}
