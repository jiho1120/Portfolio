using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    //체력/최대 체력, 마나, 공격력, 방어력, 치명타 확률, 이동 속도, 운
    public float hp { get; private set; }
    public float maxHp { get; private set; }
    
    public float att { get; private set; }
    public float armor { get; private set; }
    public float critical { get; private set; }
    public bool isCritical { get; private set; }

    public float speed { get; private set; }
    public float mp { get; private set; }
    public float maxMp { get; private set; }
    public float luck { get; private set; }

    public Stat()
    {

    }
    public Stat(Stat stat)
    {
        this.hp = stat.hp;
        maxHp = stat.hp;
        this.att = stat.att;
        this.armor = stat.armor;
        this.critical = stat.critical;
        this.speed = stat.speed;
        this.mp = mp;
        maxMp = this.mp;
        this.luck = luck;
    }

    public Stat(float hp, float att, float armor, float critical, float speed, float mp = 0, float luck = 0)
    {
        this.hp = hp;
        maxHp = this.hp;
        this.att = att;
        this.armor = armor;
        this.critical = critical;
        this.speed = speed;
        this.mp = mp;
        maxMp = this.mp;
        this.luck = luck;
    }
    
    
    public virtual void SetStat(float hp, float att, float armor, float critical, float speed, float mp = 0, float luck = 0)
    {
        this.hp = hp;
        maxHp = hp;
        this.att = att;
        this.armor = armor;
        this.critical = critical;
        this.speed = speed;
        this.mp = mp;
        maxMp = this.mp;
        this.luck = luck;
    }
    public void LevelUp(Stat stat)
    {
        stat.hp += 100;
        stat.maxHp += 100;
        stat.att = 10;
        stat.armor += 5;
        stat.critical += 10;
        stat.speed += 0.1f;
        stat.mp += 100;
        stat.maxMp += 100;
        stat.luck += 0.1f;
    }
    public bool CheckCritical(float critical)
    {
        float cri = Random.Range(0f, critical);

    }
    public float CriticalDamage(bool isCritical)
    {
        float criticalDamage = 0;
        if (isCritical)
        {
            criticalDamage = att * 2;
        }
        else
        {
            criticalDamage = att;
        }

        return criticalDamage;

    }
    public void TakeDamage(float attack, float critical)
    {
        isCritical = CheckCritical(critical);
        CriticalDamage(isCritical);
        this.hp = 
    }

    public void Attack()
    {

    }

}
