public class PlayerStat : BossStat
{
    public int luck { get; private set; }
    public int experience { get; private set; }
    public int maxExperience { get; private set; }
    public int ultimateGauge { get; private set; }
    public int maxUltimateGauge { get; private set; }

    public PlayerStat() : base()
    {
        SetValues(1,100,100,10,5,5,10,100,100,1,0,100,0,100);
    }

    public PlayerStat(int level, int health, int maxHealth, int attack, int defense, float criticalChance, float movementSpeed, int mana, int maxMana, int luck, int experience, int maxExperience, int ultimateGauge, int maxUltimateGauge)
        : base(level, health, maxHealth, attack, defense, criticalChance, movementSpeed, mana, maxMana)
    {
        this.luck = luck;
        this.experience = experience;
        this.maxExperience = maxExperience;
        this.ultimateGauge = ultimateGauge;
        this.maxUltimateGauge = maxUltimateGauge;
    }
    public void SetValues(int level, int health, int maxHealth, int attack, int defense, float criticalChance, float movementSpeed, int mana, int maxMana, int luck, int experience, int maxExperience, int ultimateGauge, int maxUltimateGauge)
    {
        base.SetValues(level, health, maxHealth, attack, defense, criticalChance, movementSpeed, mana, maxMana);
        this.luck = luck;
        this.experience = experience;
        this.maxExperience = maxExperience;
        this.ultimateGauge = ultimateGauge;
        this.maxUltimateGauge = maxUltimateGauge;
    }


}
