public class BossStat : UseManaStat
{
    public BossStat() : base()
    {

    }
    public BossStat(SOBoss data)
    {
        base.SetValues(data.objectType, data.level, data.health, data.maxHealth, data.attack, data.defense, data.criticalChance, data.movementSpeed, data.experience, data.money, data.mana, data.maxMana);
    }

}
