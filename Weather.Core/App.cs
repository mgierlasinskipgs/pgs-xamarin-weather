using MvvmCross.ViewModels;
using System.Runtime.CompilerServices;
using Weather.Core.ViewModels;

[assembly: InternalsVisibleTo("Weather.UnitTests")]
namespace Weather.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            var configuration = new IoCConfiguration();
            configuration.Initialize();

            RegisterAppStart<WeatherViewModel>();
        }
    }
}
