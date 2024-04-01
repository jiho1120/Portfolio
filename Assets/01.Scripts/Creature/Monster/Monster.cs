using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public AllEnum.MonsterType monType;
    MonsterAnimation anim;//얘는 진짜 단순히 애니메이션 출력...    
    public Vector3 dir;
    public Rigidbody rb { get; private set; }
    public bool isDeActive { get; private set; } = false;
    public bool isDead { get; private set; } = false;

    public Coroutine dieCor = null;


    #region FSM
    public AllEnum.States NowState = AllEnum.States.End;//현재상태   
    public NavMeshAgent Agent => agent;
    NavMeshAgent agent;
    MonStateMachine monStateMachine;

    #endregion

    #region 공격
    public Transform attackPos;
    public float attackDistance { get; private set; } = 4;
    float attackCoolTime = 5;
    public bool isAttack;
    public bool isHit { get; private set; } = false;
    Coroutine AttackCor = null;
    #endregion

    void Start()
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
        Init();
    }
    public void Init()
    {
        isAttack = true;
        isHit = false;
        isDead = false;
        isDeActive = false;
        monStateMachine.SetState(AllEnum.States.Idle);
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


    #region 공격
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
    #endregion

    #region  State 상태 관련
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
            Debug.Log("agent 없음");
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

    protected IEnumerator DeletObject()
    {
        float time = 0;
        // 죽고 2초 지나거나 죽고 2초전에 게임이 끝남
        while (GameManager.Instance.stageStart && time < 2)
        {
            time += Time.deltaTime;
            yield return null;
        }
        MonsterManager.Instance.ReturnToPool(this);
        dieCor = null;
    }
    public virtual void Die()
    {
        isDead = true;
        SetDeadAnim();
        dieCor = StartCoroutine(DeletObject());
    }

    // 안죽고 pool에 들어감(비활성화)
    public void DeActive() // 이거는 나중에 변수로 처리
    {
        MonsterManager.Instance.ReturnToPool(this);
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
