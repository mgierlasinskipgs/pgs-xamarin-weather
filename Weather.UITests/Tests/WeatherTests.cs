using NUnit.Framework;
using System.Linq;
using Weather.UITests.Pages;
using Xamarin.UITest;

namespace Weather.UITests.Tests
{
    [TestFixture(Platform.Android)]
    public class WeatherTests
    {
        private IApp _app;
        private readonly Platform _platform;

        public WeatherTests(Platform platform)
        {
            _platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            _app = AppInitializer.StartApp(_platform);
        }

        [TestCase("London")]
        [TestCase("Norwich")]
        [TestCase("Oxford")]
        public void Entering_city_name_should_display_weather_info(string query)
        {
            new WeatherPage(_app)
                .EnterSearchQuery(query)
                .TapSearchButton();
            
            var results = _app.WaitForElement(c => c.Marked(query));

            Assert.IsTrue(results.Any());
        }
    }
}
