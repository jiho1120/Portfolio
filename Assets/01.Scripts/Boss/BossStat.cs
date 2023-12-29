public class BossStat : Stat
{
    public float mana { get; private set; }
    public float maxMana { get; private set; }

    public BossStat() : base()
    {

    }
    public BossStat(AllEnum.ObjectType objectType, int level, float health, float maxHealth, float attack, float defense, float criticalChance, float movementSpeed, float mana, float maxMana)
        : base(objectType, level, health, maxHealth, attack, defense, criticalChance, movementSpeed)
    {
        this.mana = mana;
        this.maxMana = maxMana;
    }

    public void SetValues(AllEnum.ObjectType objectType, int level, float health, float maxHealth, float attack, float defense, float criticalChance, float movementSpeed, float mana, float maxMana)
    {
        base.SetValues(objectType,level, health, maxHealth, attack, defense, criticalChance, movementSpeed);
        this.mana = mana;
        this.maxMana = maxMana;
    }
}
