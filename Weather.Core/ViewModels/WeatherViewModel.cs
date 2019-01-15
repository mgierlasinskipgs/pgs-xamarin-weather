using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using Weather.Core.Models;
using Weather.Core.Services;

namespace Weather.Core.ViewModels
{
    public class WeatherViewModel : MvxViewModel
    {
        private readonly IWeatherService _weatherService;
        private readonly IMvxLog _log;

        public IMvxCommand SearchCommand { get; set; }

        private string _searchQuery;

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (SetProperty(ref _searchQuery, value))
                {
                    SearchCommand.RaiseCanExecuteChanged();
                }
            }
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

        private string _icon;

        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
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

            SearchCommand = new MvxAsyncCommand(SearchAction, () => !string.IsNullOrWhiteSpace(SearchQuery));
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
                Icon = $"ic_{weatherData.Icon}";
                Temperature = $"{weatherData.Temperature} °C";

                IsWeatherVisible = true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                _log.Error(e, "Error when trying to get weather data");
            }
            finally
            {
                IsLoading = false;
                SearchQuery = string.Empty;
            }
        }
    }
}
