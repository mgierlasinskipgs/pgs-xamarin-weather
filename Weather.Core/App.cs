using MvvmCross;
using MvvmCross.ViewModels;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Weather.Api.Clients;
using Weather.Core.Services;
using Weather.Core.ViewModels;

[assembly: InternalsVisibleTo("Weather.UnitTests")]
namespace Weather.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterSingleton(() => new HttpClient());
            Mvx.IoCProvider.RegisterType<IApiClient, ApiClient>();
            Mvx.IoCProvider.RegisterType<IWeatherService, WeatherService>();

            RegisterAppStart<WeatherViewModel>();
        }
    }
}
