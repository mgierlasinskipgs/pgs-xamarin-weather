using MvvmCross.Converters;
using MvvmCross.Platforms.Ios.Core;
using Weather.Core;
using Weather.iOS.Converters;

namespace Weather.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override void FillValueConverters(IMvxValueConverterRegistry registry)
        {
            base.FillValueConverters(registry);

            registry.AddOrOverwrite("IconConverter", new IconConverter());
        }
    }
}