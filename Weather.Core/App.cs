using MvvmCross;
using MvvmCross.ViewModels;
using Weather.Core.Services;
using Weather.Core.ViewModels;

namespace Weather.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterType<IWeatherService, WeatherService>();

            RegisterAppStart<WeatherViewModel>();
        }
    }
}
