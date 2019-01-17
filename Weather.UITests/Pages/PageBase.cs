using FluentAssertions;
using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Weather.UITests.Pages
{
    public class PageBase
    {
        protected IApp App { get; }

        public PageBase(IApp app)
        {
            App = app;
        }

        public void HasElementVisible(Func<AppQuery, AppQuery> selector)
        {
            var results = App.WaitForElement(selector);
            results.Should().HaveCount(1);
        }

        public void HasElementNotVisible(Func<AppQuery, AppQuery> selector)
        {
            App.WaitForNoElement(selector);
        }
    }
}
