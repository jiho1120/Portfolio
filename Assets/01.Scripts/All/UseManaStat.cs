using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseManaStat : BasicStat
{
    public float mana { get; private set; }
    public float maxMana { get; private set; }

    public UseManaStat() : base()
    {

    }
    public UseManaStat(AllEnum.ObjectType objectType, int level, float health, float maxHealth, float attack, float defense, float criticalChance, float movementSpeed, float experience, int money,float mana, float maxMana)
        : base(objectType, level, health, maxHealth, attack, defense, criticalChance, movementSpeed, experience ,money)
    {
        this.mana = mana;
        this.maxMana = maxMana;
    }

        
    public void SetValues(AllEnum.ObjectType objectType, int level, float health, float maxHealth, float attack, float defense, float criticalChance, float movementSpeed, float experience, int money, float mana, float maxMana)
    {
        base.SetValues(objectType, level, health, maxHealth, attack, defense, criticalChance, movementSpeed, experience, money);
        this.mana = mana;
        this.maxMana = maxMana;
    }
    public override void ShowInfo()
    {
        base.ShowInfo();
    }
}
