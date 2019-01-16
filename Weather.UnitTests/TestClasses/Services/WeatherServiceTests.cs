using FluentAssertions;
using Weather.Api.Clients;
using Weather.Core.Models;
using Weather.Core.Services;
using Xunit;

namespace Weather.UnitTests.TestClasses.Services
{
    public class WeatherServiceTests
    {
        [Theory]
        [InlineData(Units.Fahrenheit, "imperial")]
        [InlineData(Units.Celsius, "metric")]
        [InlineData(Units.Kelvin, "")]
        public void Converting_units_should_return_correct_api_parameter_value(Units units, string expected)
        {
            // Arrange
            var api = new ApiClient();
            var service = new WeatherService(api);

            // Act
            var value = service.GetParamValueForUnits(units);

            // Assert
            value.Should().Be(expected);
        }
    }
}
