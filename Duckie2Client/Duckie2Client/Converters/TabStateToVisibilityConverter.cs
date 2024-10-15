using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Duckie2Client.Enums;

namespace Duckie2Client.Converters;

public class TabStateToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // return value!.GetType().Name.Equals(parameter);
        var state = (TabStates)value!.GetType().GetProperty("State")?.GetValue(value)!;
        return state.Equals((TabStates)parameter!);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}