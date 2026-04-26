using WrathnillaTweaker.GameConf.WriterParser;

namespace WrathnillaTweaker.GameConf;

public class MobConfig
{
    public MobDamage Damage { get; set; } = new();
    public MobSpellDamage SpellDamage { get; set; } = new();
    public MobHealth Health { get; set; } = new();
}

public class MobDamage
{
    [PropertyName("Rate.Creature.Normal.Damage")]
    public float Normal { get; set; }

    [PropertyName("Rate.Creature.Elite.Elite.Damage")]
    public float Elite { get; set; }

    [PropertyName("Rate.Creature.Elite.RARE.Damage")]
    public float Rare { get; set; }

    [PropertyName("Rate.Creature.Elite.RAREELITE.Damage")]
    public float RareElite { get; set; }

    [PropertyName("Rate.Creature.Elite.WORLDBOSS.Damage")]
    public float WorldBoss { get; set; }
}

public class MobSpellDamage
{
    [PropertyName("Rate.Creature.Normal.SpellDamage")]
    public float Normal { get; set; }

    [PropertyName("Rate.Creature.Elite.Elite.SpellDamage")]
    public float Elite { get; set; }

    [PropertyName("Rate.Creature.Elite.RARE.SpellDamage")]
    public float Rare { get; set; }

    [PropertyName("Rate.Creature.Elite.RAREELITE.SpellDamage")]
    public float RareElite { get; set; }

    [PropertyName("Rate.Creature.Elite.WORLDBOSS.SpellDamage")]
    public float WorldBoss { get; set; }
}

public class MobHealth
{
    [PropertyName("Rate.Creature.Normal.HP")]
    public float Normal { get; set; }

    [PropertyName("Rate.Creature.Elite.Elite.HP")]
    public float Elite { get; set; }

    [PropertyName("Rate.Creature.Elite.RARE.HP")]
    public float Rare { get; set; }

    [PropertyName("Rate.Creature.Elite.RAREELITE.HP")]
    public float RareElite { get; set; }

    [PropertyName("Rate.Creature.Elite.WORLDBOSS.HP")]
    public float WorldBoss { get; set; }
}
