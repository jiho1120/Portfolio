using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour, IAttack, IDead, ILevelUp
{
    [Header("NowState는 인스펙터 창에서 건들지 마세요")]
    public AllEnum.StateEnum NowState = AllEnum.StateEnum.End;//현재상태
    public SOBoss soOriginBoss;
    public BossStat bossStat { get; private set; }
    PlayerAnimator animator; // 플레이어랑 똑같음
    public NavMeshAgent agent { get; private set; }
    public Transform characterBody;
    public Transform fist;
    Rigidbody rb;
    private float attackSpeed = 1;
    private float lastClickTime = 0f;
    private float attackCooldown = 1.5f;
    bool isLeft = false;
    bool isDead = false;
    public bool useableSKill = true;
    Coroutine HealHpMpCor;
    public bool isStop;
    public int bossLayer;
    public int PassiveCurrentNum;
    public Coroutine skillcor = null;
    Coroutine passiveCor = null;
    


public float distance { get; private set; }
    public void FirstStart()
    {
        //rb = transform.GetComponentInChildren<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<PlayerAnimator>();
        agent = GetComponent<NavMeshAgent>();
        fist = transform.GetChild(0).GetChild(3);
        animator.Starts();
        animator.SetAttackSpeed(attackSpeed);
        bossStat = new BossStat(soOriginBoss);
        HealHpMpCor = StartCoroutine(HealHpMp());
        bossLayer = 1 << LayerMask.NameToLayer("Player");
        PassiveCurrentNum = Random.Range((int)AllEnum.SkillName.Fire, (int)AllEnum.SkillName.End); // 초반 한번 설정 이것도 씬에서
    }

    public void Init()
    {
        rb.isKinematic = false;
        gameObject.transform.position = GameManager.Instance.player.transform.position + new Vector3(1, 0, 1);
        gameObject.SetActive(true);
        LevelUp(); // 능력치 세팅
        if (passiveCor == null)
        {
            passiveCor = StartCoroutine(GameManager.Instance.CallPassive(false));
        }
        GameManager.Instance.CallPassiveSkill(false);
        agent.isStopped = false;
        GetComponent<BehaviorTree>().SetInit();

    }
    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        SetMoveAnim(0, agent.velocity.z, agent.velocity.x);
    }
    public void Clear()
    {
        Stop();
    }
    public void SetAgentDirection(Vector3 targetPosition)
    {
        // 에이전트의 방향을 목표 위치로 설정
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.LookAt(transform.position + direction);
    }
    public void StartSkillTime()
    {
        skillcor = StartCoroutine(SkillTime());
    }
    IEnumerator SkillTime() // 스킬 한번에 쓰지 않고 좀 기다렸다가 쓸려고
    {
        int i = Random.Range(0, (int)AllEnum.SkillName.Gravity);

        while (true)
        {
            AllEnum.SkillName skillname = (AllEnum.SkillName)i;
            if (SkillManager.Instance.UseableSkill(skillname, false))
            {
                SkillManager.Instance.UseSKill(skillname, false);
                useableSKill = false;
                break;
            }
            else
            {
                i++;
                if (i >= (int)AllEnum.SkillName.Gravity)
                {
                    i = Random.Range(0, (int)AllEnum.SkillName.Gravity);
                }
            }
        }
        yield return new WaitForSeconds(10f);
        useableSKill = true;
        skillcor = null;
    }
    public void SetStopAndMove()
    {
        StartCoroutine(StartMoveTimer());
    }
    public IEnumerator StartMoveTimer()
    {
        agent.isStopped = true;
        isStop = true;
        yield return new WaitForSeconds(1f);
        rb.isKinematic = true;
        yield return new WaitForSeconds(1f);
        isStop = false;
        rb.isKinematic = false;
        agent.isStopped = false;

    }
    public IEnumerator HealHpMp()
    {
        while (true)
        {
            bossStat.SetHealth((bossStat.health + 1) * 0.1f);
            bossStat.SetMana((bossStat.mana + 1) * 0.1f);
            yield return new WaitForSeconds(2);
        }
    }
    public float CheckDistance()
    {
        distance = (GameManager.Instance.player.transform.position - transform.position).sqrMagnitude;
        return distance;
    }
    public void BasicAttack()
    {
        //클릭할때마다 이전시간과 비교해서 연속공격상태면 다음 주먹으로 변경하고
        //연속공격내의 시간이 아니면 첫주먹으로.

        float timeSinceLastClick = Time.time - lastClickTime;

        // 1초동안함 근데 스피드가 증가함
        // 애니메이션 스피드가 올라가서 애니메이션도 빨리 끝남
        float animTime = 1f / attackSpeed; // 바뀐 애니메이션 시간 = 애니메이션 시간(1초) / 애니메이션 스피드

        //동작하는 동안의 시간이면 되돌려보내고
        if (timeSinceLastClick <= animTime)
        {
            return;
        }
        else //그게 아니라면
        {
            if (timeSinceLastClick <= attackCooldown) //연속공격
            {
                if (isLeft)
                {
                    animator.LeftAttack();
                }
                else
                {
                    animator.RightAttack();
                }
                isLeft = !isLeft;
            }
            else
            {
                isLeft = false;
                animator.RightAttack();
            }
            lastClickTime = Time.time;

        }
    }
    public void Attack(Vector3 Tr, float Range)
    {
        Collider[] colliders = Physics.OverlapSphere(Tr, Range);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                colliders[i].GetComponent<Player>().TakeDamage(bossStat.criticalChance, bossStat.attack);
            }
        }
    }
    public void AttackRange() // 애니메이션에 넣음
    {
        Attack(fist.position, 1f);
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
    public void TakeDamage(float critical, float attack)
    {
        if (!isDead)
        {
            Hit();
            Debug.Log("맞음");
            float damage = Mathf.Max(CriticalDamage(critical, attack) - (bossStat.defense * 0.5f), 1f); // 최소 데미지 1
            float hp = bossStat.health - damage;
            bossStat.SetHealth(hp);
            if (hp <= 0)
            {
                Debug.Log("피가 0이하");
                bossStat.SetHealth(0);
                //Dead(false);
            }
        }
        else
        {
            Debug.Log("이미 죽었어");
        }
    }
    public void Dead(bool force)
    {
        isDead = true;
        if (passiveCor != null)
        {
            StopCoroutine(passiveCor);
            passiveCor = null;
        }
        Debug.Log("죽음");
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void LevelUp()
    {
        if (bossStat != null)
        {
            bossStat.LevelUp(); // 레벨 1만 올리는 함수
            StatUp();
        }
    }

    public void StatUp()
    {
        bossStat.SetMaxHealth((bossStat.level * 1000) + soOriginBoss.maxHealth);
        bossStat.SetHealth(soOriginBoss.maxHealth);
        bossStat.SetAttack((bossStat.level * 100) + soOriginBoss.attack);
        bossStat.SetDefence((bossStat.level * 100) + soOriginBoss.defense);
        bossStat.SetcriticalChance((bossStat.level * 2.5f) + soOriginBoss.criticalChance);
        bossStat.SetSpeed((bossStat.level * 0.5f) + soOriginBoss.movementSpeed);
        bossStat.SetExp((bossStat.level * 1000) + soOriginBoss.experience);
        bossStat.SetMoney((bossStat.level * 1000) + soOriginBoss.money);
        bossStat.SetMaxMana((bossStat.level * 1000) + soOriginBoss.maxMana);
        bossStat.SetMana(bossStat.maxMana);

    }

    #region Anim
    public void SetMoveAnim(float speed, float z, float x)
    {
        animator.WalkOrRun(speed); // 1or 1.5
        animator.MoveAnim(z, x);
    }
    public void Hit()
    {
        animator.SetHit();
    }

    #endregion
}
