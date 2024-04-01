using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, Initialize
{
    public AllEnum.MonsterType monType;
    MonsterAnimation anim;//��� ��¥ �ܼ��� �ִϸ��̼� ���...    
    public Vector3 dir;
    public Rigidbody rb { get; private set; }
    public bool isDeActive { get; private set; } = false;
    public bool isDead { get; private set; } = false;

    public Coroutine deActiveCor = null;
    MonsterData monsterData;
    StatData monStat;

    #region FSM
    public AllEnum.States NowState = AllEnum.States.End;//�������   
    public NavMeshAgent Agent => agent;
    NavMeshAgent agent;
    MonStateMachine monStateMachine;

    #endregion

    #region ����
    public Transform attackPos;
    public float attackDistance { get; private set; } = 4;
    float attackCoolTime = 5;
    public bool isAttack;
    public bool isHit { get; private set; } = false;
    Coroutine AttackCor = null;
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
        rb.isKinematic = true;
        Agent.isStopped = true;
        isAttack = false;
        isHit = false;
        isDead = true; // �̰� �������� ���� �ɸ� �׷��� true���� �������
        isDeActive = true;
        MonsterManager.Instance.ReturnToPool(this);
    }


    #region Set
    public void SetIsAttack(bool on)
    {
        isAttack = on;
    }
    public void SetIsHit(bool on)
    {
        isHit = on;
    }
    public void SetIsDead(bool on)
    {
        isDead = on;
    }
    public void SetIsDeActive(bool on)
    {
        isDeActive = on;
    }
    #endregion


    #region ���� & �ǰ�
    public void Attack() // �ִϸ��̼ǿ� ����
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
        float damage = CriticalDamage(critical, attack) - (monStat.defense * 0.5f); // ���� ���� �߰�
        monStat.hp -= damage;
        if (monStat.hp <= 0)
        {
            monStat.hp = 0;
            isDead = true;
        }
    }
    #endregion

    #region  State ���� ����
    public Vector3 CheckDir()
    {
        dir = GameManager.Instance.player.transform.position - transform.position;
        dir.y = 0;

        return dir;
    }
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


    public void Hit()
    {
        SetHitAnim();
        agent.isStopped = true;
    }

    IEnumerator DeActiveTime()
    {
        yield return new WaitForSeconds(2f);
        isDeActive = true;
        if (deActiveCor != null)
        {
            deActiveCor = null;
        }
    }
     
    public virtual void Die()
    {
        Agent.isStopped = true;
        rb.isKinematic = true;
        isDead = true;
        SetDeadAnim();
        if (deActiveCor == null)
        {
            deActiveCor = StartCoroutine(DeActiveTime());
        }
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
