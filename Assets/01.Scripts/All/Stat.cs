using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    //레벨, 체력,최대 체력, 공격력, 방어력, 치명타 확률, 이동 속도,     마나,최대 마나 ,    운, 경험치,최대 경험치, 궁극기 게이지, 최대 궁극기 게이지
    public int level { get; private set; }
    public float health { get; private set; }
    public float maxHealth { get; private set; }
    public float attack { get; private set; }
    public float defense { get; private set; }
    public float criticalChance { get; private set; }
    public float movementSpeed { get; private set; }
    
    public Stat()
    {

    }

    public Stat(int level, float health, float maxHealth, float attack, float defense, float criticalChance, float movementSpeed)
    {
        this.level = level;
        this.health = health;
        this.maxHealth = maxHealth;
        this.attack = attack;
        this.defense = defense;
        this.criticalChance = criticalChance;
        this.movementSpeed = movementSpeed;
    }

    public void SetValues(int level, float health, float maxHealth, float attack, float defense, float criticalChance, float movementSpeed)
    {
        this.level = level;
        this.health = health;
        this.maxHealth = maxHealth;
        this.attack = attack;
        this.defense = defense;
        this.criticalChance = criticalChance;
        this.movementSpeed = movementSpeed;
    }

    
    

}
