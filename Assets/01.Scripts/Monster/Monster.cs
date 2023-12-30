using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IAttack
{
    public AllEnum.States NowState = AllEnum.States.End;//현재상태
    MonsterAnimation anim; //얘는 진짜 단순히 애니메이션 출력...
    NavMeshAgent agent;
    MONStateMachine monStateMachine;

    public SOMonster soOriginMonster;
    MonsterStat monsterStat;

    private void Start()
    {
        anim = GetComponent<MonsterAnimation>();
        anim.SetInit();
        agent = GetComponent<NavMeshAgent>();
        monsterStat = new MonsterStat();
        monStateMachine = GetComponent<MONStateMachine>();
        monStateMachine.SetInit();
        monsterStat.SetValues(soOriginMonster); // 가져올때만 쓰고  렙업시 스탯 올려주는  함수 만들어서 스탯 올려주기 json 파일로 저장하기  
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


    public void Idle()
    {
        anim.Idle();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    public void Move(Vector3 vec, bool anim = false)
    {
        if (anim)
        {
            SetMoveAnim();
        }
        agent.isStopped = false;
        agent.SetDestination(vec);
        //조건에 따라서
        //Walk의 애니메이션이 움직임이 좀 시원치 않을 수도 있음..
        //때문에 SetMoveAnim으로 따로 분리함.        
    }
    public void SetMoveAnim()
    {
        anim.Walk(true);
    }

    public virtual void Dead()
    {
        Debug.Log("죽음");
    }
}
