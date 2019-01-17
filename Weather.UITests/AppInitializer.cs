using Xamarin.UITest;

namespace Weather.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .ApkFile("../../../Weather.Droid/bin/Release/Weather.Droid-Signed.apk")
                    .StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}