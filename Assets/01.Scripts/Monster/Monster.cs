using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IAttack
{
    public SOMonster soOriginMonster;
    MonsterStat monsterStat;

    private void Start()
    {
        monsterStat = new MonsterStat();

        monsterStat.SetValues(soOriginMonster); // 가져올때만 쓰고  렙업시 스탯 올려주는  함수 만들어서 스탯 올려주기 json 파일로 저장하기  
        Debug.Log("몬스터");
        monsterStat.ShowInfo();
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
        Debug.Log("몬스터 맞음");

    }

    public virtual void Dead()
    {
        //Destroy(this.gameObject.transform.GetChild(0).gameObject); 고쳐야함
        Debug.Log("죽음");
    }
}
