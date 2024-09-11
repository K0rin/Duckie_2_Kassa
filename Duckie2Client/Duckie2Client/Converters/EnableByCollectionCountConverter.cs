using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Duckie2Client.Converters;

public class EnableByCollectionCountConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return false;

        if (!value.GetType().Name.Contains("Collection"))
        // Converter used for the wrong type.
            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        
        Console.WriteLine(((ICollection)value).Count);
        return ((ICollection)value).Count > 0;

    }


    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}