using System.Globalization;
using System.IO;
using System.Reflection;

namespace WrathnillaTweaker.GameConf.WriterParser;

public class Parser
{
    public T Parse<T>(string filePath) where T : new()
    {
        var lines = File.ReadAllLines(filePath);
        var props = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            if (trimmed.Length == 0 || trimmed.StartsWith('#') || trimmed.StartsWith('['))
                continue;

            var idx = trimmed.IndexOf('=');
            if (idx == -1) throw new FormatException($"Invalid config line: {trimmed}");

            props[trimmed[..idx].Trim()] = trimmed[(idx + 1)..].Trim();
        }

        return (T)BuildConfig(typeof(T), props);
    }

    private static object BuildConfig(Type type, Dictionary<string, string> props)
    {
        var instance = Activator.CreateInstance(type)
            ?? throw new InvalidOperationException($"Cannot create instance of {type.Name}");

        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!property.CanWrite) continue;

            var attr = property.GetCustomAttribute<PropertyNameAttribute>();
            if (attr != null)
            {
                if (!props.TryGetValue(attr.Identifier, out var raw))
                    throw new KeyNotFoundException($"Missing config key: {attr.Identifier}");
                property.SetValue(instance, Coerce(raw, property.PropertyType));
            }
            else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
            {
                property.SetValue(instance, BuildConfig(property.PropertyType, props));
            }
        }

        return instance;
    }

    private static object Coerce(string raw, Type type)
    {
        if (type == typeof(int)) return int.Parse(raw, CultureInfo.InvariantCulture);
        if (type == typeof(float)) return float.Parse(raw, CultureInfo.InvariantCulture);
        if (type == typeof(double)) return double.Parse(raw, CultureInfo.InvariantCulture);
        if (type == typeof(string)) return raw;
        throw new NotSupportedException($"Unsupported property type: {type.Name}");
    }
}
