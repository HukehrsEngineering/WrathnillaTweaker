using System.Globalization;
using System.Windows.Controls;

namespace WrathnillaTweaker.Converters;

public class FloatValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var input = value?.ToString()?.Replace(',', '.');
        if (float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
            return ValidationResult.ValidResult;
        return new ValidationResult(false, "Enter a valid number.");
    }
}