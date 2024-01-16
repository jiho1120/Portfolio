using UnityEngine;

public class MonsterStat : BasicStat
{
    public MonsterStat() : base()
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
