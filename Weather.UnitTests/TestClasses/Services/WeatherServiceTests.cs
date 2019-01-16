using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Api.Clients;
using Weather.Api.Models;
using Weather.Core.Models;
using Weather.Core.Services;
using Xunit;

namespace Weather.UnitTests.TestClasses.Services
{
    public class WeatherServiceTests
    {
        [Fact]
        public async Task Getting_weather_for_valid_city_should_return_proper_data()
        {
            // Arrange
            var response = new CurrentWeather
            {
                Name = "London",
                Main = new Main { Temp = 30 },
                Weather = new List<Api.Models.Weather>
                {
                    new Api.Models.Weather { Main = "Dizzy", Icon = "01d" }
                }
            };
            
            var service = new WeatherService(GetClientWithResponse(response));

            // Act
            var data = await service.GetCurrentWeather("London");

            // Assert
            data.CityName = "London";
            data.Description.Should().Be("Dizzy");
            data.Icon.Should().Be("01d");
            data.Temperature.Should().Be(30);
        }

        [Fact]
        public async Task Getting_weather_for_city_without_main_element_should_return_zero_temperature()
        {
            // Arrange
            var response = new CurrentWeather();
            var service = new WeatherService(GetClientWithResponse(response));

            // Act
            var data = await service.GetCurrentWeather("London");

            // Assert
            data.Temperature.Should().Be(0);
        }

        [Fact]
        public async Task Getting_weather_for_city_without_any_weather_element_should_return_empty_icon_and_description()
        {
            // Arrange
            var response = new CurrentWeather();
            var service = new WeatherService(GetClientWithResponse(response));

            // Act
            var data = await service.GetCurrentWeather("London");

            // Assert
            data.Description.Should().BeNull();
            data.Icon.Should().BeNull();
        }

        [Fact]
        public async Task Getting_weather_for_city_with_many_weather_elements_should_return_icon_and_description_from_first()
        {
            // Arrange
            var response = new CurrentWeather
            {
                Weather = new List<Api.Models.Weather>
                {
                    new Api.Models.Weather { Main = "Rainy", Icon = "02d" },
                    new Api.Models.Weather { Main = "Dizzy", Icon = "01d" },
                }
            };

            var service = new WeatherService(GetClientWithResponse(response));

            // Act
            var data = await service.GetCurrentWeather("London");

            // Assert
            data.Description.Should().Be("Rainy");
            data.Icon.Should().Be("02d");
        }

        [Theory]
        [InlineData(Units.Fahrenheit, "imperial")]
        [InlineData(Units.Celsius, "metric")]
        [InlineData(Units.Kelvin, "")]
        public void Converting_units_should_return_correct_api_parameter_value(Units units, string expected)
        {
            // Arrange
            var api = new ApiClient(new HttpClient());
            var service = new WeatherService(api);

            // Act
            var value = service.GetParamValueForUnits(units);

            // Assert
            value.Should().Be(expected);
        }

        private IApiClient GetClientWithResponse(CurrentWeather response)
        {
            var mock = new Mock<IApiClient>();
            mock.Setup(x => x
                .GetCurrentWeather(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(response);

            return mock.Object;
        }
    }
}
