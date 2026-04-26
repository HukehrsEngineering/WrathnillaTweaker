using System.IO;
using WrathnillaTweaker.GameConf.WriterParser;

namespace WrathnillaTweaker;

public class AppConf
{
    private const string AppConfFile = "./app.conf";

    [PropertyName("GameplayConfigPath")]
    public string GameplayConfigPath { get; set; } = string.Empty;

    public void Save()
    {
        File.WriteAllText(AppConfFile, $"GameplayConfigPath = {GameplayConfigPath}");
    }

    public static AppConf LoadOrDefault()
    {
        if (File.Exists(AppConfFile))
            return new Parser().Parse<AppConf>(AppConfFile);

        const string defaultPath = "./configs/gameplay.conf";
        return new AppConf { GameplayConfigPath = File.Exists(defaultPath) ? defaultPath : string.Empty };
    }
}
