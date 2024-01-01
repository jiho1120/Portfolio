using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    public Vector3 dir;
    public Transform TargetTr {  get; private set; }

    public bool isAttack = false;  // 공격 쿨타임을 줘서 시간이 되면 트루로 바꾸게
    public bool isHit = false;
    public bool isDead = false;
    public float coolAttackTime = 0;


    private void Start()
    {
        anim = GetComponent<MonsterAnimation>();
        anim.SetInit();
        agent = GetComponent<NavMeshAgent>();
        monStateMachine = GetComponent<MONStateMachine>();
        monStateMachine.SetInit();
        monsterStat = new MonsterStat();
        monsterStat.SetValues(soOriginMonster); // 가져올때만 쓰고  렙업시 스탯 올려주는  함수 만들어서 스탯 올려주기 json 파일로 저장하기  
        //monsterStat.ShowInfo();
        isAttack = true;

    }

    public void SetTarget(Transform tr)
    {
        this.TargetTr = tr;
    }
    public Vector3 CheckDir()
    {
        dir = TargetTr.position - this.transform.position;
        return dir;
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
        float damage = CriticalDamage(critical, attack) - (this.monsterStat.defense * 0.5f); // 몬스터 스탯 추가
        float hp = this.monsterStat.health - damage;
        monsterStat.SetHealth(hp);
        if (this.monsterStat.health < 0)
        {
            monsterStat.SetHealth(0);
            isDead = true;
        }
        Debug.Log($"{monsterStat.health}");
    }

    public void Idle()
    {
        SetIdelAnim();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    public void Move(Vector3 vec)
    {
        agent.isStopped = false;
        agent.SetDestination(vec);
        SetMoveAnim();
    }
    public void Attack()
    {
        agent.isStopped = true;
        SetAttackAnim();
    }
    public void SetAttackState()
    {
        coolAttackTime += Time.deltaTime;
        if (coolAttackTime >= 5f)
        {
            coolAttackTime = 0;
            isAttack = true;
        }
    }
    
    public void Hit()
    {
        SetHitAnim();
        agent.isStopped = true;
    }
    public virtual void Dead()
    {
        SetDeadAnim();
        Debug.Log("죽음");
        Invoke("DeletObject",3f);
    }
    public void DeletObject()
    {
        GameManager.Instance.monsterPool.ReturnObjectToPool(this);
    }
    public void SetIdelAnim()
    {
        anim.Idle();
    }
    public void SetMoveAnim()
    {
        anim.Walk();
    }
    public void SetAttackAnim()
    {
        anim.Attack();
    }
    public void SetHitAnim()
    {
        anim.Hit();
    }
    public void SetDeadAnim()
    {
        anim.Die();
    }


}
