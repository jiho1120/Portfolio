using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStat
{
    //����, ü��,�ִ� ü��, ���ݷ�, ����, ġ��Ÿ Ȯ��, �̵� �ӵ�,     ����,�ִ� ���� ,    ��, ����ġ,�ִ� ����ġ, �ñر� ������, �ִ� �ñر� ������
    protected AllEnum.ObjectType objectType { get; private set; }
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
        if (this.health > maxHealth)
        {
            this.health = maxHealth;
        }
    }
    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }
    public void AddMaxHealth(float maxHealth)
    {
        this.maxHealth += maxHealth;
    }
    public void AddHp(float helath)
    {
        this.health += helath;
        if (this.health > maxHealth)
        {
            this.health = maxHealth;
        }
    }
    public void SetSpeed(float speed)
    {
        this.movementSpeed = speed;
    }
    public void AddSpeed(float speed)
    {
        this.movementSpeed += speed;
    }
    public void AddAttack(float att)
    {
        this.attack += att;
    }
    public void AddDefence(float dff)
    {
        this.defense += dff;
        if (this.defense < 1)
        {
            this.defense = 1;
        }
    }
    public void AddcriticalChance(float chance)
    {
        this.criticalChance += chance;
    }

    public void AddExp(float exp)
    {
        this.experience += exp;
    }
    public void SetExp(float exp)
    {
        experience = exp;
    }
    public void AddMoney(int money)
    {
        this.money += money;
    }
    public void SetMoney(int money)
    {
        this.money = money;
    }
    public void LevelUp()
    {
        this.level++;
    }
    public virtual void AddAnything(AllEnum.PlyerStat statName, float effect)
    {
        switch (statName)
        {
            case AllEnum.PlyerStat.maxHealth:
                maxHealth += effect;
                break;
            case AllEnum.PlyerStat.attack:
                attack += effect;
                break;
            case AllEnum.PlyerStat.defense:
                defense += effect;
                break;
            case AllEnum.PlyerStat.criticalChance:
                criticalChance += effect;
                break;
            case AllEnum.PlyerStat.movementSpeed:
                movementSpeed += effect;
                break;
            case AllEnum.PlyerStat.experience:
                experience += effect;
                break;
            default:
                break;
        }
        Debug.Log($"{statName}�� {effect}��ŭ ����ƽ��ϴ�");
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
