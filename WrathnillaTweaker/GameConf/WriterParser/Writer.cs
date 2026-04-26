using System.Globalization;
using System.IO;
using System.Reflection;

namespace WrathnillaTweaker.GameConf.WriterParser;

public class Writer
{
    public void Write(string filePath, object config, bool backup = true)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Config file not found: {filePath}");

        if (backup)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            File.Copy(filePath, $"{filePath}.bck_{timestamp}");
        }

        var props = CollectProps(config);
        var lines = File.ReadAllLines(filePath);
        var updated = lines.Select(line =>
        {
            var trimmed = line.Trim();
            if (trimmed.Length == 0 || trimmed.StartsWith('#') || trimmed.StartsWith('['))
                return line;

            var eqIdx = line.IndexOf('=');
            if (eqIdx == -1) return line;

            var key = line[..eqIdx].Trim();
            if (!props.TryGetValue(key, out var newValue)) return line;

            var afterEquals = line[(eqIdx + 1)..];
            var leadingSpaces = afterEquals.Length - afterEquals.TrimStart().Length;
            var spacing = leadingSpaces > 0 ? afterEquals[..leadingSpaces] : " ";
            return $"{line[..(eqIdx + 1)]}{spacing}{newValue}";
        });

        File.WriteAllText(filePath, string.Join("\n", updated) + "\n");
    }

    private static Dictionary<string, string> CollectProps(object obj)
    {
        var result = new Dictionary<string, string>();
        foreach (var property in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var value = property.GetValue(obj);
            if (value == null) continue;

            var attr = property.GetCustomAttribute<PropertyNameAttribute>();
            if (attr != null)
            {
                result[attr.Identifier] = FormatValue(value);
            }
            else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
            {
                foreach (var (k, v) in CollectProps(value))
                    result[k] = v;
            }
        }
        return result;
    }

    private static string FormatValue(object value) => value switch
    {
        float f => f.ToString("0.####", CultureInfo.InvariantCulture),
        double d => d.ToString("0.####", CultureInfo.InvariantCulture),
        _ => value.ToString() ?? string.Empty,
    };
}
