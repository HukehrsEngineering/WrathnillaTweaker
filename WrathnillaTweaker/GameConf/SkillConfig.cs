using WrathnillaTweaker.GameConf.WriterParser;

namespace WrathnillaTweaker.GameConf;

public class SkillConfig
{
    [PropertyName("MaxPrimaryTradeSkill")]
    public int MaxTradeSkills { get; set; }

    public SkillGains SkillGains { get; set; } = new();
}

public class SkillGains
{
    [PropertyName("SkillGain.Crafting")]
    public int Crafting { get; set; }

    [PropertyName("SkillGain.Defense")]
    public int Defense { get; set; }

    [PropertyName("SkillGain.Gathering")]
    public int Gathering { get; set; }

    [PropertyName("SkillGain.Weapon")]
    public int Weapon { get; set; }
}
