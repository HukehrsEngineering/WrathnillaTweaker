using System.Globalization;
using System.Windows.Data;

namespace WrathnillaTweaker.Converters;

[ValueConversion(typeof(float), typeof(string))]
public class FloatToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is float f)
            return f.ToString("0.####", CultureInfo.InvariantCulture);
        return value?.ToString() ?? string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (float.TryParse(value?.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
            return result;
        throw new FormatException($"'{value}' is not a valid decimal number.");
    }
}
