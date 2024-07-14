using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class Monster : Creature, IProduct
{
    #region 기본
    public AllEnum.MonsterType monType;
    MonsterAnimation anim;//얘는 진짜 단순히 애니메이션 출력...    
    #endregion

    #region 팩토리
    [SerializeField] private string productName = "Monster";
    public string ProductName { get => productName; set => productName = value; }
    #endregion

    #region 오브젝트 풀
    private IObjectPool<Monster> objectPool;
    public IObjectPool<Monster> ObjectPool { set => objectPool = value; }
    #endregion

    #region FSM
    public AllEnum.States NowState = AllEnum.States.End;//현재상태   
    public NavMeshAgent Agent => agent;
    NavMeshAgent agent;
    MonStateMachine monStateMachine;

    #endregion

    #region 공격
    public float attackDistance { get; private set; } = 4;
    float attackCoolTime = 5;

    // 공격 가능한지 체크
    public bool isAttackable { get; private set; }
    public bool isHit { get; private set; } = false;


    Coroutine AttackCor = null;
    #endregion

    #region Set
    public void SetIsHit(bool on)
    {
        isHit = on;
    }

    public void SetIsDeActive(bool on)
    {
        isDeActive = on;
    }

    public override void GetAttToData()
    {
        Stat.attack = DataManager.Instance.gameData.monsterData.monsterStat.attack;
    }

    #endregion


    public override void Init()
    {
        base.Init();
        gameObject.name = productName;
        id = GameManager.Instance.CreatureId;
        GameManager.Instance.CreatureId++;
        anim = GetComponent<MonsterAnimation>();
        anim?.SetInit();
        agent = GetComponent<NavMeshAgent>();
        monStateMachine = GetComponent<MonStateMachine>();
        monStateMachine?.Init();
        Stat = new StatData(DataManager.Instance.gameData.monsterData.monsterStat);
        EnemyLayerMask = 1 << LayerMask.NameToLayer("Player");
        AttackRange = 1f;
        Activate();
    }
    public override void Activate()
    {
        base.Activate();
        Stat.SetStat(DataManager.Instance.gameData.monsterData.monsterStat);
        isDeActive = false;
        deActiveCor = null;
        // 스킬 쓸때만 false로 바꾸기 그래야 밀림
        rb.isKinematic = true;
        IsKnockback = false;
        Agent.isStopped = false;
        isAttackable = true;
        isHit = false;
        isDead = false;
        monStateMachine.SetState(AllEnum.States.Idle);

    }
    

    #region Die , DeActive

    public override void Deactivate()
    {
        if (deActiveCor == null)
        {
            deActiveCor = StartCoroutine(DeactivateRoutine(MonsterManager.Instance.timeoutDelay));
        }
    }
    // 죽음 -> 몇초뒤에 디액티브 부름
    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        isDeActive = true;
        deActiveCor = null;
        rb.isKinematic = true;
        objectPool.Release(this); // 원래 베이스로 오브젝트가 꺼지는걸 받아야하는데 얘는 오브젝트풀에이미 꺼지는게 있음
    }

    
    public override void Die()
    {
        if (((ActiveSkill)SkillManager.Instance.GetSkill(GameManager.Instance.player, AllEnum.SkillName.Gravity)).IsAvailable) // 궁극기 상태에서 죽으면 안줌 -> 주게되면 거의 무한으로씀
        {
            GameManager.Instance.player.SetUltimate(Stat.ultimateGauge);
        }
        Agent.isStopped = true;
        rb.isKinematic = true;
        SetDeadAnim();
        ItemManager.Instance.DropRandomItem(this);
        GameManager.Instance.player.AddExp(Stat.experience);
        GameManager.Instance.SetKillMon(GameManager.Instance.killMon + 1);
        UIManager.Instance.UpdateMonsterCount(GameManager.Instance.killMon);

        if (deActiveCor == null)
        {
            deActiveCor = StartCoroutine(DeactivateRoutine(MonsterManager.Instance.timeoutDelay));
        }
    }

    #endregion

    #region 레벨 관련
    
    public override void StatUp() // 매니저에서 일괄적으로
    {

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
            isAttackable = false;
            SetAttackAnim();
            yield return new WaitForSeconds(attackCoolTime);
            isAttackable = true;
            AttackCor = null;
        }
    }

    public override void ImplementTakeDamage()
    {
        isHit = true;
    }
    public void Hit()
    {
        SetHitAnim();
        agent.isStopped = true;
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
        anim.AttackAnim(isAttackable);
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
