using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Monster : Creature, Initialize
{
    #region 기본
    public AllEnum.MonsterType monType;
    MonsterAnimation anim;//얘는 진짜 단순히 애니메이션 출력...    
    public Rigidbody rb { get; private set; }
    MonsterData monsterData;
    StatData monStat;
    #endregion


    #region die, deAct
    public bool isDeActive { get; private set; } = false;
    Coroutine deActiveCor = null;
    Vector3 itempos = new Vector3(0, 1, 0);
    #endregion

    #region FSM
    public AllEnum.States NowState = AllEnum.States.End;//현재상태   
    public NavMeshAgent Agent => agent;
    NavMeshAgent agent;
    MonStateMachine monStateMachine;
    public Vector3 dir;

    #endregion

    #region 공격
    public Transform attackPos;
    public float attackDistance { get; private set; } = 4;
    float attackCoolTime = 5;
    public bool isAttack;
    public bool isHit { get; private set; } = false;
    Coroutine AttackCor = null;
    #endregion

    #region Set
    public void SetIsAttack(bool on)
    {
        isAttack = on;
    }
    public void SetIsHit(bool on)
    {
        isHit = on;
    }
    
    public void SetIsDeActive(bool on)
    {
        isDeActive = on;
    }
    #endregion

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
            monStateMachine = GetComponent<MonStateMachine>();
            monStateMachine.Init();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        monsterData = DataManager.Instance.gameData.monsterData;
        monStat = monsterData.monsterStat;
        isAttack = true;
        isHit = false;
        isDead = false;
        isDeActive = false;
        monStateMachine.SetState(AllEnum.States.Idle);
        //monStat.PrintStatData();
    }
    public void Deactivate()
    {
        isDeActive = true;
        deActiveCor = null;
        rb.isKinematic = true;
        Agent.isStopped = true;
        isAttack = false;
        isHit = false;
        isDead = true; // 이게 죽음보다 먼저 걸림 그래서 true여도 상관없음
        isDeActive = true;
        MonsterManager.Instance.ReturnToPool(this);
    }
    


    #region 공격 & 피격
    public void Attack() // 애니메이션에 넣음
    {
        Collider[] cols = AttackRange(attackPos.position, 0.5f);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].CompareTag("Player"))
            {
                cols[i].GetComponent<Player>().TakeDamage(DataManager.Instance.gameData.monsterData.monsterStat.critical, DataManager.Instance.gameData.monsterData.monsterStat.attack);
            }
        }
        if (AttackCor == null)
        {
            AttackCor = StartCoroutine(SetAttackCoolTime());
        }
    }
    public Collider[] AttackRange(Vector3 Tr, float Range)
    {
        return Physics.OverlapSphere(Tr, Range);
    }

    IEnumerator SetAttackCoolTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                1f);
            isAttack = false;
            SetAttackAnim();
            yield return new WaitForSeconds(attackCoolTime);
            isAttack = true;
            AttackCor = null;
        }
    }
    public bool CheckCritical(float critical)
    {
        bool isCritical = Random.Range(0f, 100f) < critical;
        return isCritical;
    }
    public float CriticalDamage(float critical, float attack)
    {
        float criticalDamage;
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
    public void TakeDamage(float critical, float attack)
    {
        if (isDead)
        {
            return;
        }
        isHit = true;
        float damage = CriticalDamage(critical, attack) - (monStat.defense * 0.5f); // 몬스터 스탯 추가
        monStat.hp -= damage;
        if (monStat.hp <= 0)
        {
            monStat.hp = 0;
            isDead = true;
        }
    }
    public void Hit()
    {
        SetHitAnim();
        agent.isStopped = true;
    }
    #endregion

    #region 시야
    public Vector3 CheckDir()
    {
        dir = GameManager.Instance.player.transform.position - transform.position;
        dir.y = 0;

        return dir;
    }
    #endregion

    #region 움직임
    public void Idle()
    {
        if (agent != null)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            SetIdelAnim();
        }
        else
        {
            Debug.Log("agent 없음");
        }
    }

    public void Move(Vector3 vec)
    {
        agent.isStopped = false;
        agent.SetDestination(vec);
        SetMoveAnim();
    }
    #endregion

    #region Die , DeActive
    public virtual void Die()
    {
        GameManager.Instance.SetKillMon(GameManager.Instance.killMon + 1);
        UIManager.Instance.UpdateMonsterCount(GameManager.Instance.killMon);
        Agent.isStopped = true;
        rb.isKinematic = true;
        SetDeadAnim();
        if (deActiveCor == null)
        {
            deActiveCor = StartCoroutine(DeActiveTime());
        }
        GameManager.Instance.CheakStageClear();
    }
    IEnumerator DeActiveTime()
    {
        yield return new WaitForSeconds(2f);
        isDeActive = true;
        deActiveCor = null;
    }
    public void DropRandomItem()
    {
        int itemIndex = Random.Range(0, 3);
        if (itemIndex == 0)
        {
            DataManager.Instance.gameData.playerData.playerStat.money += monStat.money;
        }
        else
        {
            if (itemIndex == 1) // 장비
            {
                itemIndex = Random.Range(0, 7);
            }
            else if (itemIndex == 2) // 물약
            {
                itemIndex = Random.Range(101, 104);

            }
            ItemManager.Instance.DropItem(itemIndex, transform.position + itempos);
        }
    }

    #endregion

    #region 

    #endregion

    #region 
    #endregion

    #region anim 볼 필요없음
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
        anim.AttackAnim(isAttack);
    }

    public void SetHitAnim()
    {
        anim.Hit();
    }
    public void SetDeadAnim()
    {
        anim.Die();
    }
    #endregion
}
