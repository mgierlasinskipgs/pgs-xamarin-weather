using Xamarin.UITest;

namespace Weather.UITests.Pages
{
    public class WeatherPage
    {
        private readonly IApp _app;

        public WeatherPage(IApp app)
        {
            _app = app;
        }

        public WeatherPage EnterSearchQuery(string searchQuery)
        {
            _app.EnterText(c => c.Class("EditText"), searchQuery);
            return this;
        }

        public WeatherPage TapSearchButton()
        {
            _app.Tap(c => c.Class("Button"));
            return this;
        }
    }
}
