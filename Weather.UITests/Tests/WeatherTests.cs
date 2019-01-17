using NUnit.Framework;
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
        public void Entering_valid_city_name_should_display_weather_info(string query)
        {
            // Act
            var page = new WeatherPage(_app)
                .EnterSearchQuery(query)
                .TapSearchButton();
            
            // Assert
            page.HasElementVisible(c => c.Marked(query));
        }

        [Test]
        public void Entering_invalid_city_name_should_display_error_message()
        {
            // Act
            var page = new WeatherPage(_app)
                .EnterSearchQuery("InvalidCity")
                .TapSearchButton();

            // Assert
            page.HasElementVisible(c => c.Marked("Weather for city InvalidCity not found."));
        }

        [Test]
        public void Entering_valid_city_name_should_show_progress_bar()
        {
            // Act
            var page = new WeatherPage(_app)
                .EnterSearchQuery("London")
                .TapSearchButton();

            // Assert
            page.HasElementVisible(page.ProgressBar);
        }

        [Test]
        public void Button_should_be_disabled_when_search_query_is_empty()
        {
            // Act
            var page = new WeatherPage(_app)
                .TapSearchButton();

            // Assert
            page.HasElementNotVisible(page.Icon);
        }
    }
}
