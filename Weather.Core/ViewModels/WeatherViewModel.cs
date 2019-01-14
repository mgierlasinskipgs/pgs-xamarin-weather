using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Weather.Core.Services;

namespace Weather.Core.ViewModels
{
    public class WeatherViewModel : MvxViewModel
    {
        private readonly IWeatherService _weatherService;
        private readonly IMvxLog _log;

        public ICommand ShowCurrentWeatherCommand { get; set; }

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
        
        private double _temperature;

        public double Temperature
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

        public WeatherViewModel(IWeatherService weatherService, IMvxLog log)
        {
            _weatherService = weatherService;
            _log = log;

            ShowCurrentWeatherCommand = new MvxAsyncCommand(ShowCurrentWeatherAction);
        }

        private async Task ShowCurrentWeatherAction()
        {
            ErrorMessage = string.Empty;

            try
            {
                var weatherData = await _weatherService.GetCurrentWeather(CityName);

                Description = weatherData.Description;
                Temperature = weatherData.Temperature;
            }
            catch (Exception e)
            {
                _log.Error(e, "Error when trying to get weather data");
                ErrorMessage = e.Message;
            }
        }
    }
}
