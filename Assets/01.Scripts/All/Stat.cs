using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    //����, ü��,�ִ� ü��, ���ݷ�, ����, ġ��Ÿ Ȯ��, �̵� �ӵ�,     ����,�ִ� ���� ,    ��, ����ġ,�ִ� ����ġ, �ñر� ������, �ִ� �ñر� ������
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
