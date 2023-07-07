using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace LuckyFish.FileManager.Converters;

public class SizeStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is long size)
        {
            if (size == 0)
                return "";
            
            int i = 0;
            string[] name = {"B","KB","MB","GB","TB" };
            while (true)
            {
                if (size <= 1024) break;
                size /= 1024;
                i += 1;
            }
            return $"{size}{name[i]}";
        }
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}