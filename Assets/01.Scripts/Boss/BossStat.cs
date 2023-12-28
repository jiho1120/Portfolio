public class BossStat : Stat
{
    public int mana { get; private set; }
    public int maxMana { get; private set; }

    public BossStat() : base()
    {

    }

    public BossStat(int level, int health, int maxHealth, int attack, int defense, float criticalChance, float movementSpeed, int mana, int maxMana)
        : base(level, health, maxHealth, attack, defense, criticalChance, movementSpeed)
    {
        this.mana = mana;
        this.maxMana = maxMana;
    }

    public void SetValues(int level, int health, int maxHealth, int attack, int defense, float criticalChance, float movementSpeed, int mana, int maxMana)
    {
        base.SetValues(level, health, maxHealth, attack, defense, criticalChance, movementSpeed);
        this.mana = mana;
        this.maxMana = maxMana;
    }
}
