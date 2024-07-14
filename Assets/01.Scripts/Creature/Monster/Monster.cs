using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class Monster : Creature, IProduct
{
    #region �⺻
    public AllEnum.MonsterType monType;
    MonsterAnimation anim;//��� ��¥ �ܼ��� �ִϸ��̼� ���...    
    #endregion

    #region ���丮
    [SerializeField] private string productName = "Monster";
    public string ProductName { get => productName; set => productName = value; }
    #endregion

    #region ������Ʈ Ǯ
    private IObjectPool<Monster> objectPool;
    public IObjectPool<Monster> ObjectPool { set => objectPool = value; }
    #endregion

    #region FSM
    public AllEnum.States NowState = AllEnum.States.End;//�������   
    public NavMeshAgent Agent => agent;
    NavMeshAgent agent;
    MonStateMachine monStateMachine;

    #endregion

    #region ����
    public float attackDistance { get; private set; } = 4;
    float attackCoolTime = 5;

    // ���� �������� üũ
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
        // ��ų ������ false�� �ٲٱ� �׷��� �и�
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
    // ���� -> ���ʵڿ� ���Ƽ�� �θ�
    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        isDeActive = true;
        deActiveCor = null;
        rb.isKinematic = true;
        objectPool.Release(this); // ���� ���̽��� ������Ʈ�� �����°� �޾ƾ��ϴµ� ��� ������ƮǮ���̹� �����°� ����
    }

    
    public override void Die()
    {
        if (((ActiveSkill)SkillManager.Instance.GetSkill(GameManager.Instance.player, AllEnum.SkillName.Gravity)).IsAvailable) // �ñر� ���¿��� ������ ���� -> �ְԵǸ� ���� �������ξ�
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

    #region ���� ����
    
    public override void StatUp() // �Ŵ������� �ϰ�������
    {

    }
    #endregion
    #region ���� & �ǰ�
    public override void Attack() // �ִϸ��̼ǿ� ����
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



    #region ������
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
            Debug.Log("agent ����");
        }
    }

    public void Move(Vector3 vec)
    {
        agent.isStopped = false;
        agent.SetDestination(vec);
        SetMoveAnim();
    }
    #endregion

    #region anim �� �ʿ����
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
