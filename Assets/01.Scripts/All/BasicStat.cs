using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStat
{
    //����, ü��,�ִ� ü��, ���ݷ�, ����, ġ��Ÿ Ȯ��, �̵� �ӵ�,     ����,�ִ� ���� ,    ��, ����ġ,�ִ� ����ġ, �ñر� ������, �ִ� �ñر� ������
    public AllEnum.ObjectType objectType { get; private set; }
    public int level { get; private set; }
    public float health { get; private set; }
    public float maxHealth { get; private set; }
    public float attack { get; private set; }
    public float defense { get; private set; }
    public float criticalChance { get; private set; }
    public float movementSpeed { get; private set; }
    public float experience { get; private set; }
    public int money { get; private set; }



    public BasicStat()
    {

    }
    

    public BasicStat(AllEnum.ObjectType objectType, int level, float health, float maxHealth, float attack, float defense, float criticalChance, float movementSpeed, float experience, int money)
    {
        this.objectType = objectType;
        this.level = level;
        this.health = health;
        this.maxHealth = maxHealth;
        this.attack = attack;
        this.defense = defense;
        this.criticalChance = criticalChance;
        this.movementSpeed = movementSpeed;
        this.experience = experience;
        this.money = money;
    }

    public void SetValues(AllEnum.ObjectType objectType, int level, float health, float maxHealth, float attack, float defense, float criticalChance, float movementSpeed, float experience, int money)
    {
        this.objectType = objectType;
        this.level = level;
        this.health = health;
        this.maxHealth = maxHealth;
        this.attack = attack;
        this.defense = defense;
        this.criticalChance = criticalChance;
        this.movementSpeed = movementSpeed;
        this.experience = experience;
        this.money = money;
    }


    public void SetHealth(float helath)
    {
        this.health = helath;
    }

    public void SetSpeed(float speed)
    {
        this.movementSpeed = speed;
    }
    public void KillMonster(float experience, int money)
    {
        this.experience += experience;
        this.money += money;
    }

    public virtual void ShowInfo()
    {
        Debug.Log(this.objectType);
        Debug.Log(this.level);
        Debug.Log(this.health);
        Debug.Log(this.maxHealth);
        Debug.Log(this.attack);
        Debug.Log(this.defense);
        Debug.Log(this.criticalChance);
        Debug.Log(this.movementSpeed);
    }

}
