public class BossStat : UseManaStat
{

    public BossStat() : base()
    {

    }

    public BossStat(AllEnum.ObjectType objectType, int level, float health, float maxHealth, float attack, float defense, float criticalChance, float movementSpeed,float experience, int money, float mana, float maxMana) : base(objectType, level, health, maxHealth, attack, defense, criticalChance, movementSpeed, experience, money, mana, maxMana)
    {
    }
}
