using UnityEngine;

public class MonsterStat : BasicStat
{
    public MonsterStat() : base()
    {

    }

    public MonsterStat(AllEnum.ObjectType objectType, int level, float health, float maxHealth, float attack, float defense, float criticalChance, float movementSpeed, float experience
    ,int money)
        : base(objectType, level, health, maxHealth, attack, defense, criticalChance, movementSpeed, experience, money)
    {
        
    }
    public void SetValues(SOMonster soMonster)
    {
        base.SetValues(soMonster.objectType, soMonster.level, soMonster.health, soMonster.maxHealth, soMonster.attack, soMonster.defense, soMonster.criticalChance, soMonster.movementSpeed, soMonster.experience, soMonster.money);
    }

    public override void ShowInfo()
    {
        base.ShowInfo();
    }
    
}
