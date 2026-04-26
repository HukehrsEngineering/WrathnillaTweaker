using WrathnillaTweaker.GameConf.WriterParser;

namespace WrathnillaTweaker.GameConf;

public class ExperienceConfig
{
    [PropertyName("Rate.XP.Kill")]
    public float Kill { get; set; }

    [PropertyName("Rate.XP.Quest")]
    public float Quest { get; set; }

    [PropertyName("Rate.XP.Explore")]
    public float Exploration { get; set; }

    [PropertyName("Rate.XP.Pet")]
    public float Pet { get; set; }
}
