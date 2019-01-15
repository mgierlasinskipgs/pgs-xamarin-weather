using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Weather.Core.Models;
using Weather.Core.Services;

namespace Weather.Core.ViewModels
{
    public class WeatherViewModel : MvxViewModel
    {
        private readonly IWeatherService _weatherService;
        private readonly IMvxLog _log;

        public ICommand SearchCommand { get; set; }

        private string _searchQuery;

        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }

        private string _cityName;

        public string CityName
        {
            get => _cityName;
            set => SetProperty(ref _cityName, value);
        }

        private string _description;

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        
        private string _temperature;

        public string Temperature
        {
            get => _temperature;
            set => SetProperty(ref _temperature, value);
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private bool _isWeatherVisible;

        public bool IsWeatherVisible
        {
            get => _isWeatherVisible;
            set => SetProperty(ref _isWeatherVisible, value);
        }

        public WeatherViewModel(IWeatherService weatherService, IMvxLog log)
        {
            _weatherService = weatherService;
            _log = log;

            SearchCommand = new MvxAsyncCommand(SearchAction);
        }

        private async Task SearchAction()
        {
            ErrorMessage = string.Empty;
            IsLoading = true;
            IsWeatherVisible = false;

            try
            {
                var weatherData = await _weatherService.GetCurrentWeather(SearchQuery, Units.Celsius);

                CityName = weatherData.CityName;
                Description = weatherData.Description;
                Temperature = $"{weatherData.Temperature} °C";

                IsWeatherVisible = true;
            }
            catch (Exception e)
            {
                _log.Error(e, "Error when trying to get weather data");
                ErrorMessage = e.Message;
            }
            finally
            {
                IsLoading = false;
                SearchQuery = string.Empty;
            }
        }
    }
}
