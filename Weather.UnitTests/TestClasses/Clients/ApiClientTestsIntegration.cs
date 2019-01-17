using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Api.Clients;
using Weather.UnitTests.Assertions;
using Weather.UnitTests.TestData;
using Xunit;
using static Weather.Api.Resources.ErrorMessages;

namespace Weather.UnitTests.TestClasses.Clients
{
    public class ApiClientTestsIntegration
    {
        private readonly HttpClient _httpClient = new HttpClient();

        [Theory]
        [MemberData(nameof(WeatherTestData.ValidSearchParameters), MemberType = typeof(WeatherTestData))]
        public async Task Calling_api_with_valid_parameters_should_succeed(string query, string units)
        {
            // Arrange
            var apiClient = new ApiClient(_httpClient);

            // Act
            var result = await apiClient.GetCurrentWeather(query, units);

            // Assert
            result.Name.Should().Be(query);
            result.Main.Should().NotBeNull();
            result.Weather.Should().NotBeNull().And.HaveCountGreaterThan(0);
        }

        [Fact]
        public void Calling_api_with_non_existing_city_should_fail()
        {
            // Arrange
            var apiClient = new ApiClient(_httpClient);

            // Act
            Func<Task> act = async () =>
            {
                var result = await apiClient.GetCurrentWeather("NonExitingCity", "metric");
            };

            // Assert
            act.Should().Throw<ApiException>().WithError(HttpStatusCode.NotFound, string.Format(NotFound, "NonExitingCity"));
        }

        [Fact]
        public void Calling_api_without_api_key_should_fail()
        {
            // Arrange
            var apiClient = new ApiClient(_httpClient)
            {
                ApiKey = string.Empty
            };

            // Act
            Func<Task> act = async () =>
            {
                var result = await apiClient.GetCurrentWeather("London", "metric");
            };

            // Assert
            act.Should().Throw<ApiException>().WithError(HttpStatusCode.Unauthorized, "Unauthorized");
        }
    }
}
