using System;
using System.Globalization;
using System.Windows.Data;

namespace WeatherApp.Converters;

public class BoolToRainConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool valueAsBool = (bool)value;

        if (valueAsBool)
            return "Currently rainning";

        return "Currently not rainning";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string valueAsString = (string)value;

        if (string.IsNullOrWhiteSpace(valueAsString) && valueAsString is "Currently rainning")
            return true;

        return false;
    }
}
