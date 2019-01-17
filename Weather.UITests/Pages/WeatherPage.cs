using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Weather.UITests.Pages
{
    public class WeatherPage : PageBase
    {
        public Func<AppQuery, AppQuery> SearchEdit => e => e.Class("EditText");
        public Func<AppQuery, AppQuery> SearchButton => e => e.Class("Button");
        public Func<AppQuery, AppQuery> Icon => e => e.Class("ImageView");
        public Func<AppQuery, AppQuery> ProgressBar => e => e.Class("ProgressBar");
        
        public WeatherPage(IApp app) 
            : base(app)
        {   
        }

        public WeatherPage EnterSearchQuery(string searchQuery)
        {
            App.EnterText(SearchEdit, searchQuery);
            return this;
        }

        public WeatherPage TapSearchButton()
        {
            App.Tap(SearchButton);
            return this;
        }
    }
}
