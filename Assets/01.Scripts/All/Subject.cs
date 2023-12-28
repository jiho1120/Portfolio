using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    public void ChangeHealth(float health, float damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }

    }
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
            Debug.Log("크리 뜸");
        }
        else
        {
            criticalDamage = attack;
            Debug.Log("크리 안 뜸");

        }

        return criticalDamage;
    }

    public virtual void Hit(float critical, float attack, float health, float defense)
    {
        TakeDamage(critical, attack, health, defense);
    }

    /// <summary>
    ///  데미지 입음(기본공격만 생각함)
    /// </summary>
    /// <param name="critical">적의 크리티컬 확률</param>
    /// <param name="attack">적의 공격력</param>
    /// <param name="health">나의 피</param>
    /// <param name="defense">나의 방어력</param>
    public virtual void TakeDamage(float critical, float attack, float health, float defense)
    {
        float damage = CriticalDamage(critical, attack) - (defense * 0.5f);
        ChangeHealth(health , damage);

    }

    public virtual void Dead()
    {
        //Destroy(this.gameObject.transform.GetChild(0).gameObject); 고쳐야함
        Debug.Log("죽음");
    }
}
