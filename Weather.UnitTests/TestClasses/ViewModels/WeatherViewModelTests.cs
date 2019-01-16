using FluentAssertions;
using MvvmCross.Tests;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Core.Models;
using Weather.Core.Services;
using Weather.Core.ViewModels;
using Xunit;
using static Weather.UnitTests.Concrete.HttpHelper;

namespace Weather.UnitTests.TestClasses.ViewModels
{
    public class WeatherViewModelTests : ViewModelTestsBase
    {
        [Fact]
        public async Task Executing_search_command_for_valid_data_should_set_correct_property_values()
        {
            // Arrange
            RegisterServiceWithTestData(new WeatherData
            {
                CityName = "London",
                Description = "Dizzy",
                Icon = "01d",
                Temperature = 20
            });

            var viewModel = Ioc.IoCConstruct<WeatherViewModel>();
            viewModel.SearchQuery = "London";

            // Act
            await viewModel.SearchCommand.ExecuteAsync();

            // Assert
            viewModel.CityName.Should().Be("London");
            viewModel.Description.Should().Be("Dizzy");
            viewModel.Icon.Should().Be("ic_01d");
            viewModel.Temperature.Should().Be("20 °C");

            viewModel.IsWeatherVisible.Should().BeTrue("weather should be visible after data is loaded");
            viewModel.IsLoading.Should().BeFalse("loader should be hidden after data is loaded");

            viewModel.SearchQuery.Should().BeEmpty("search query should be cleared after search is performed");
            viewModel.ErrorMessage.Should().BeEmpty("there should be no error displayed");
        }

        [Fact]
        public async Task Executing_search_command_for_not_found_api_response_should_set_correct_error_message()
        {
            // Arrange
            var apiClient = GetApiClientForResponse(new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound });
            Ioc.RegisterSingleton<IWeatherService>(new WeatherService(apiClient));
            
            var viewModel = Ioc.IoCConstruct<WeatherViewModel>();
            viewModel.SearchQuery = "InvalidCity";

            // Act
            await viewModel.SearchCommand.ExecuteAsync();

            // Assert
            viewModel.ErrorMessage.Should().Be("Weather for city InvalidCity not found.");
            viewModel.IsWeatherVisible.Should().BeFalse();
        }

        [Fact]
        public async Task Executing_search_command_for_empty_api_response_should_set_correct_error_message()
        {
            // Arrange
            var apiClient = GetApiClientForResponse(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });
            Ioc.RegisterSingleton<IWeatherService>(new WeatherService(apiClient));

            var viewModel = Ioc.IoCConstruct<WeatherViewModel>();
            viewModel.SearchQuery = "London";

            // Act
            await viewModel.SearchCommand.ExecuteAsync();

            // Assert
            viewModel.ErrorMessage.Should().Be("Response has no content.");
            viewModel.IsWeatherVisible.Should().BeFalse();
        }

        [Fact]
        public void Executing_search_command_for_empty_search_query_should_be_impossible()
        {
            // Arrange
            RegisterServiceWithTestData(new WeatherData());

            var viewModel = Ioc.IoCConstruct<WeatherViewModel>();

            // Act
            viewModel.SearchQuery = "";

            // Assert
            viewModel.SearchCommand.CanExecute().Should().BeFalse();
        }

        [Fact]
        public void Providing_search_query_should_raise_can_execute_and_enable_command()
        {
            // Arrange
            RegisterServiceWithTestData(new WeatherData());

            var viewModel = Ioc.IoCConstruct<WeatherViewModel>();
            viewModel.SearchCommand.ListenForRaiseCanExecuteChanged();

            // Act
            viewModel.SearchQuery = "London";

            // Assert
            viewModel.SearchCommand.RaisedCanExecuteChanged().Should().BeTrue();
            viewModel.SearchCommand.CanExecute().Should().BeTrue();
        }
    }
}
