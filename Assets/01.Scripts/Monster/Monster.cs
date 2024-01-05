using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IAttack, IDead
{
    public AllEnum.MonsterType monType;
    public AllEnum.States NowState = AllEnum.States.End;//현재상태
    MonsterAnimation anim; //얘는 진짜 단순히 애니메이션 출력...
    NavMeshAgent agent;
    public NavMeshAgent Agent => agent;
    MONStateMachine monStateMachine;
    public SOMonster soOriginMonster;
    MonsterStat monsterStat;
    public Vector3 dir;
    public Transform targetTr;
    public GameObject explosionEffect;

    public Transform attackPos;
    public bool isAttack = false;  // 공격 쿨타임을 줘서 시간이 되면 트루로 바꾸게
    public bool isHit = false;
    public bool isDead = false;
    public float coolAttackTime = 0;
    public float rotationSpeed = 5f;

    
    public void Init()
    {
        
        if (anim == null)
        {
            anim = GetComponent<MonsterAnimation>();
            anim.SetInit();
        }

        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();

        }
        if (monStateMachine == null)
        {
            monStateMachine = GetComponent<MONStateMachine>();
            monStateMachine.SetInit();
        }

        if (monsterStat == null)
        {
            monsterStat = new MonsterStat();
        }
        //monsterStat.ShowInfo();
        isAttack = false;
        isHit = false;
        monsterStat.SetValues(soOriginMonster);
        isDead = false;
        monStateMachine.SetState(AllEnum.States.Idle);
    }
    
    

    public Vector3 CheckDir()
    {
        targetTr = GameManager.Instance.player.transform;
        dir = targetTr.position - this.transform.position;
        dir.y = 0;

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
        }
        else
        {
            criticalDamage = attack;

        }

        return criticalDamage;
    }
    public virtual void Attack(Transform Tr, float Range)
    { 
        Collider[] colliders = Physics.OverlapSphere(Tr.position, Range);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                colliders[i].GetComponent<Player>().TakeDamage(monsterStat.criticalChance, monsterStat.attack);
            }
        }
    }
    public void AttackRange() // 애니메이션에 넣음
    {
        Attack(attackPos, 0.5f);
    }


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
        //Debug.Log($"{monsterStat.health}");
    }

    public void Idle()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        //this.transform.LookAt(GameManager.Instance.transform.position);
        SetIdelAnim();
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
        isDead = true;
        agent.isStopped = true;
        SetDeadAnim();
        
        Invoke("DeletObject",3f);
    }
    public void DeletObject()
    {
        if (this.monType == AllEnum.MonsterType.Explosion)
        {
            Explosion();
        }
        MonsterManager.Instance.MonsterPool().ReturnObjectToPool(this);
    }
    public void Explosion()
    {
        Attack(this.transform, 2f);
        Instantiate(explosionEffect, this.transform.position + new Vector3(0,1,0), Quaternion.identity);
    }

    // 아래는 애니메이션만 불러오는것
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

    public bool IsDead()
    {
        return isDead;
    }

   
}
