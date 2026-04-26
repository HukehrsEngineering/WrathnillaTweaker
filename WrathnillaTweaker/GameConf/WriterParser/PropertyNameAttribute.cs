namespace WrathnillaTweaker.GameConf.WriterParser;

[AttributeUsage(AttributeTargets.Property)]
public sealed class PropertyNameAttribute(string identifier) : Attribute
{
    public string Identifier { get; } = identifier;
}
