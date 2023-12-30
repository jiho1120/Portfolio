using UnityEngine;

public class MonsterStat : BasicStat
{
    public MonsterStat() : base()
    {

    }

    public MonsterStat(AllEnum.ObjectType objectType, int level, float health, float maxHealth, float attack, float defense, float criticalChance, float movementSpeed)
        : base(objectType, level, health, maxHealth, attack, defense, criticalChance, movementSpeed)
    {
        
    }
    public void SetValues(SOMonster soMonster)
    {
        base.SetValues(soMonster.objectType, soMonster.level, soMonster.health, soMonster.maxHealth, soMonster.attack, soMonster.defense, soMonster.criticalChance, soMonster.movementSpeed);
    }

    public override void ShowInfo()
    {
        base.ShowInfo();
    }
}
