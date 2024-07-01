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

    #region die, deAct
    public bool isDeActive { get; private set; } = false;
    Coroutine deActiveCor = null;
    #endregion

    #region FSM
    public AllEnum.States NowState = AllEnum.States.End;//�������   
    public NavMeshAgent Agent => agent;
    NavMeshAgent agent;
    MonStateMachine monStateMachine;
    public Vector3 dir;

    #endregion

    #region ����
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

    public override void GetAttToData()
    {
        Stat.attack = DataManager.Instance.gameData.monsterData.monsterStat.attack;
    }

    #endregion


    public virtual void Init()
    {
        gameObject.name = productName;
        id = GameManager.Instance.CreatureId;
        GameManager.Instance.CreatureId++;
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
        base.Activate();
        Stat.SetStat(DataManager.Instance.gameData.monsterData.monsterStat);
        isDeActive = false;
        deActiveCor = null;
        // ��ų ������ false�� �ٲٱ� �׷��� �и�
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
        base.Deactivate();
        isDeActive = true;
        deActiveCor = null;
        rb.isKinematic = true;
    }
    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        objectPool.Release(this);
    }

    public void AddPlayerMoney()
    {
        GameManager.Instance.player.AddMoney(Stat.money);
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
            isAttack = false;
            SetAttackAnim();
            yield return new WaitForSeconds(attackCoolTime);
            isAttack = true;
            AttackCor = null;
        }
    }

    public override void implementTakeDamage()
    {
        isHit = true;
    }
    public void Hit()
    {
        SetHitAnim();
        agent.isStopped = true;
    }
    #endregion

    #region �þ�
    public Vector3 CheckDir()
    {
        dir = GameManager.Instance.player.transform.position - transform.position;
        dir.y = 0;

        return dir;
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



    #region 

    #endregion

    #region 
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
