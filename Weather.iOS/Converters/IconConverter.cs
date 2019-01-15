using MvvmCross.Converters;
using System;
using System.Globalization;
using UIKit;

namespace Weather.iOS.Converters
{
    public class IconConverter : MvxValueConverter<string, UIImage>
    {
        protected override UIImage Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return UIImage.FromFile($"{value}.png");
        }
    }
}