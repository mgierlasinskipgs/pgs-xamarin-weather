using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;
using Weather.Core.Services;

namespace Weather.Core.ViewModels
{
    public class WeatherViewModel : MvxViewModel
    {
        private readonly IWeatherService _weatherService;
        
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

        public WeatherViewModel(IWeatherService weatherService)
        {
            _weatherService = weatherService;

            ShowCurrentWeatherCommand = new MvxAsyncCommand(ShowCurrentWeatherAction);
        }

        private async Task ShowCurrentWeatherAction()
        {
            var weatherData = await _weatherService.GetCurrentWeather(CityName);

            Description = weatherData.Description;
            Temperature = weatherData.Temperature;
        }
    }
}
