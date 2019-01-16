using Moq;
using MvvmCross.Base;
using MvvmCross.Commands;
using MvvmCross.Tests;
using Weather.Core.Models;
using Weather.Core.Services;

namespace Weather.UnitTests.TestClasses.ViewModels
{
    public class ViewModelTestsBase : MvxIoCSupportingTest
    {
        public ViewModelTestsBase()
        {
            Setup();
        }

        protected override void AdditionalSetup()
        {
            MvxSingletonCache.Instance.Settings.AlwaysRaiseInpcOnUserInterfaceThread = false;

            var helper = new MvxUnitTestCommandHelper();
            Ioc.RegisterSingleton<IMvxCommandHelper>(helper);
        }

        protected void RegisterServiceWithTestData(WeatherData testData)
        {
            Ioc.RegisterSingleton(GetServiceWithResult(testData));
        }

        private IWeatherService GetServiceWithResult(WeatherData result)
        {
            var mock = new Mock<IWeatherService>();
            mock.Setup(x => x
                .GetCurrentWeather(It.IsAny<string>(), It.IsAny<Units>()))
                .ReturnsAsync(result);

            return mock.Object;
        }
    }
}
