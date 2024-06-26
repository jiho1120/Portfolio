using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class Monster : Creature, IProduct
{
    #region 기본
    public AllEnum.MonsterType monType;
    MonsterAnimation anim;//얘는 진짜 단순히 애니메이션 출력...    
    public Rigidbody rb { get; private set; }
    #endregion

    #region 팩토리
    [SerializeField] private string productName = "Monster";
    public string ProductName { get => productName; set => productName = value; }
    #endregion

    #region 오브젝트 풀
    private IObjectPool<Monster> objectPool;
    public IObjectPool<Monster> ObjectPool { set => objectPool = value; }
    
    #endregion

    #region die, deAct
    public bool isDeActive { get; private set; } = false;
    Coroutine deActiveCor = null;
    #endregion

    #region FSM
    public AllEnum.States NowState = AllEnum.States.End;//현재상태   
    public NavMeshAgent Agent => agent;
    NavMeshAgent agent;
    MonStateMachine monStateMachine;
    public Vector3 dir;

    #endregion

    #region 공격
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

    
    public virtual void Init()
    {
        gameObject.name = productName;
        anim = GetComponent<MonsterAnimation>();
        anim?.SetInit();
        agent=GetComponent<NavMeshAgent>();
        monStateMachine=GetComponent<MonStateMachine>();
        monStateMachine?.Init();
        rb = GetComponent<Rigidbody>();
        Stat = new StatData(DataManager.Instance.gameData.monsterData.monsterStat);
        EnemyLayerMask = 1 << LayerMask.NameToLayer("Player");
        Activate();
    }
    public override void Activate()
    {
        Stat.SetStat(DataManager.Instance.gameData.monsterData.monsterStat);
        isDeActive = false;
        deActiveCor = null;
        // 스킬 쓸때만 false로 바꾸기 그래야 밀림
        rb.isKinematic = true;
        Agent.isStopped = false;
        isAttack = true;
        isHit = false;
        isDead = false;
        isDeActive = false;
        monStateMachine.SetState(AllEnum.States.Idle);
    }

    #region Die , DeActive
    public override void Die()
    {
        GameManager.Instance.SetKillMon(GameManager.Instance.killMon + 1);
        UIManager.Instance.UpdateMonsterCount(GameManager.Instance.killMon);
        Agent.isStopped = true;
        rb.isKinematic = true;
        SetDeadAnim();
        ItemManager.Instance.DropRandomItem(this);

        if (deActiveCor == null)
        {
            deActiveCor = StartCoroutine(DeactivateRoutine(MonsterManager.Instance.timeoutDelay));
        }
        GameManager.Instance.CheakStageClear();
    }
    public override void Deactivate()
    {
        if (deActiveCor == null)
        {
            deActiveCor = StartCoroutine(DeactivateRoutine(MonsterManager.Instance.timeoutDelay));
        }
    }
    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        isDeActive = true;
        deActiveCor = null;
        objectPool.Release(this);
    }

    public void AddPlayerMoney()
    {
        GameManager.Instance.player.AddMoney(Stat.money);
    }

    

    #endregion
    #region 공격 & 피격
    public override void Attack() // 애니메이션에 넣음
    {
        base.Attack();
        if (AttackCor == null)
        {
            AttackCor = StartCoroutine(SetAttackCoolTime());
        }
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


    public override void TakeDamage(float att)
    {
        if (isDead)
        {
            return;
        }
        isHit = true;
        float damage = CriticalDamage(att) - (Stat.defense * 0.5f); // 몬스터 스탯 추가
        Stat.hp -= damage;

        if (Stat.hp <= 0)
        {
            Stat.hp = 0;
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
