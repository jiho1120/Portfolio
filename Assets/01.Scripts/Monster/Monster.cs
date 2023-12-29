using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IAttack
{
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
    public virtual void Hit(float critical, float attack)
    {
        TakeDamage(critical, attack);
    }
    /// <summary>
    ///  데미지 입음(기본공격만 생각함)
    /// </summary>
    /// <param name="critical">적의 크리티컬 확률</param>
    /// <param name="attack">적의 공격력</param>

    public virtual void TakeDamage(float critical, float attack)
    {
        //float damage = CriticalDamage(critical, attack) - (defense * 0.5f); // 몬스터 스탯 추가
        //health -= damage;
        //if (health < 0)
        //{
        //    health = 0;
        //}

    }

    public virtual void Dead()
    {
        //Destroy(this.gameObject.transform.GetChild(0).gameObject); 고쳐야함
        Debug.Log("죽음");
    }
}
