using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;

namespace LuckyFish.FileManager.Converters;


public class BitmapValueConverter : IValueConverter
{
    public static BitmapValueConverter Instance = new ();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null)
            return null;
        if (value is string rawUri && targetType.IsAssignableFrom(typeof(Bitmap)))
            return new Bitmap(rawUri);
        throw new NotSupportedException();
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}