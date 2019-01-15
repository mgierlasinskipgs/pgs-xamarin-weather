using Android.App;
using Android.OS;
using Android.Views;
using MvvmCross.Platforms.Android.Views;
using Weather.Core.ViewModels;

namespace Weather.Droid.Views
{
    [Activity(Label = "@string/app_name", MainLauncher = false)]
    public class WeatherView : MvxActivity<WeatherViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.AppTheme);

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.WeatherView);
        }
    }
}