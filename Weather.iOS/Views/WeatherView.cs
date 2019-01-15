
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using System;
using Weather.Core.ViewModels;

namespace Blank.Views
{
    [MvxFromStoryboard]
    public partial class WeatherView : MvxViewController<WeatherViewModel>
    {
        public WeatherView(IntPtr handle) : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            activityIndicator.StartAnimating();

            var set = this.CreateBindingSet<WeatherView, WeatherViewModel>();
            set.Bind(searchTextField).To(vm => vm.SearchQuery);
            set.Bind(searchButton).To(vm => vm.SearchCommand);
            set.Bind(headerLabel).To(vm => vm.CityName);
            set.Bind(descriptionLabel).To(vm => vm.Description);
            set.Bind(imageLabel).To(vm => vm.Icon).WithConversion("IconConverter");
            set.Bind(temperatureLabel).To(vm => vm.Temperature);
            set.Bind(errorLabel).To(vm => vm.ErrorMessage);
            
            set.Bind(headerLabel)
                .For("Visibility")
                .To(vm => vm.IsWeatherVisible)
                .WithConversion("Visibility");

            set.Bind(descriptionLabel)
                .For("Visibility")
                .To(vm => vm.IsWeatherVisible)
                .WithConversion("Visibility");

            set.Bind(imageLabel)
                .For("Visibility")
                .To(vm => vm.IsWeatherVisible)
                .WithConversion("Visibility");

            set.Bind(temperatureLabel)
                .For("Visibility")
                .To(vm => vm.IsWeatherVisible)
                .WithConversion("Visibility");

            set.Bind(activityIndicator)
                .For("Visibility")
                .To(vm => vm.IsLoading)
                .WithConversion("Visibility");

            set.Apply();
        }
        
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }
    }
}