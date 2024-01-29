using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour, IAttack, IDead, ILevelUp
{
    protected bool isDead = false;


    public abstract void Attack(Vector3 Tr, float Range);


    public bool CheckCritical(float critical)
    {
        bool isCritical = Random.Range(0f, 100f) < critical;
        return isCritical;
    }


    public float CriticalDamage(float critical, float attack)
    {
        float criticalDamage = 0;
        if (CheckCritical(critical))
        {
            criticalDamage = attack * 2;
        }
        else
        {
            criticalDamage = attack;

        }

        return criticalDamage;
    }

    public abstract void Dead(bool force);


    public bool IsDead()
    {
        return isDead;
    }
    public void SetDead(bool _bool)
    {
        isDead = _bool;
    }


    public abstract void LevelUp();


    public abstract void StatUp();


    public abstract void TakeDamage(float critical, float attack);
    
}
