using MvvmCross;
using System.Net.Http;
using Weather.Api.Clients;
using Weather.Core.Services;

namespace Weather.Core
{
    public class IoCConfiguration
    {
        public void Initialize()
        {
            Mvx.IoCProvider.RegisterSingleton(() => new HttpClient());
            Mvx.IoCProvider.RegisterType<IApiClient, ApiClient>();
            Mvx.IoCProvider.RegisterType<IWeatherService, WeatherService>();
        }
    }
}
