using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Duckie2Client.Converters;

public class TabStateToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value!.GetType().Name.Equals(parameter);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}