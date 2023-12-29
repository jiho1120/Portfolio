public class MonsterStat : Stat
{
    public MonsterStat() : base()
    {

    }

    public MonsterStat(AllEnum.ObjectType objectType, int level, float health, float maxHealth, float attack, float defense, float criticalChance, float movementSpeed)
        : base(objectType, level, health, maxHealth, attack, defense, criticalChance, movementSpeed)
    {
        
    }
}
