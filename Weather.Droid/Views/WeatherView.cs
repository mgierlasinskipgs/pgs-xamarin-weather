using Android.App;
using Android.OS;
using MvvmCross.Platforms.Android.Views;
using Weather.Core.ViewModels;

namespace Weather.Droid.Views
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class WeatherView : MvxActivity<WeatherViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.WeatherView);
        }
    }
}