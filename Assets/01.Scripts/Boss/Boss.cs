using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.GridLayoutGroup;

public class Boss : MonoBehaviour, IAttack, IDead, ILevelUp
{
    [Header("NowState�� �ν����� â���� �ǵ��� ������")]
    public AllEnum.StateEnum NowState = AllEnum.StateEnum.End;//�������
    public SOBoss soOriginBoss;
    public BossStat bossStat { get; private set; }
    PlayerAnimator animator; // �÷��̾�� �Ȱ���
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
    public Coroutine weaknessCor = null;
    public Coroutine timingCor = null;

    public bool isHit { get; private set; }
    public bool haveTiming { get; private set; }




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
        PassiveCurrentNum = Random.Range((int)AllEnum.SkillName.Fire, (int)AllEnum.SkillName.End); // �ʹ� �ѹ� ���� �̰͵� ������
    }

    public void Init()
    {
        gameObject.SetActive(true);
        isDead = false;
        rb.isKinematic = false;
        gameObject.transform.position = GameManager.Instance.player.transform.position + new Vector3(3, 0, 3);
        gameObject.transform.LookAt(GameManager.Instance.player.transform.position);
        LevelUp(); // �ɷ�ġ ����
        SetHaveTiming(false);

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
        // ������Ʈ�� ������ ��ǥ ��ġ�� ����
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.LookAt(transform.position + direction);
    }
    public void StartSkillTime()
    {
        skillcor = StartCoroutine(SkillTime());
    }
    IEnumerator SkillTime() // ��ų �ѹ��� ���� �ʰ� �� ��ٷȴٰ� ������
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
        //Ŭ���Ҷ����� �����ð��� ���ؼ� ���Ӱ��ݻ��¸� ���� �ָ����� �����ϰ�
        //���Ӱ��ݳ��� �ð��� �ƴϸ� ù�ָ�����.

        float timeSinceLastClick = Time.time - lastClickTime;

        // 1�ʵ����� �ٵ� ���ǵ尡 ������
        // �ִϸ��̼� ���ǵ尡 �ö󰡼� �ִϸ��̼ǵ� ���� ����
        float animTime = 1f / attackSpeed; // �ٲ� �ִϸ��̼� �ð� = �ִϸ��̼� �ð�(1��) / �ִϸ��̼� ���ǵ�

        //�����ϴ� ������ �ð��̸� �ǵ���������
        if (timeSinceLastClick <= animTime)
        {
            return;
        }
        else //�װ� �ƴ϶��
        {
            if (timeSinceLastClick <= attackCooldown) //���Ӱ���
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
    public void AttackRange() // �ִϸ��̼ǿ� ����
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
            if (!isHit)
            {
                Debug.Log(isHit);
                Debug.Log(haveTiming);
                Debug.Log(timingCor);
                if (haveTiming && timingCor == null)
                {
                    timingCor = StartCoroutine(CheckTiming());
                    StartCoroutine(HitCool());
                }
                Hit();
                Debug.Log("����");
                float damage = Mathf.Max(CriticalDamage(critical, attack) - (bossStat.defense * 0.5f), 1f); // �ּ� ������ 1
                float hp = bossStat.health - damage;
                bossStat.SetHealth(hp);
                if (hp <= 0)
                {
                    Debug.Log("�ǰ� 0����");
                    bossStat.SetHealth(0);
                }
            }
            else
            {
                Debug.Log("���� ��Ÿ��");
            }
        }
        else
        {
            Debug.Log("�̹� �׾���");
        }
    }
    public void StartWeak()
    {
        weaknessCor = StartCoroutine(StartWeakness());
    }
    public void StopWeak()
    {
        StopCoroutine(weaknessCor);
        weaknessCor = null;

    }
    public IEnumerator StartWeakness()
    {
        while (true)
        {
            if (!isHit)
            {
                UiManager.Instance.StartShrike();
                timingCor = null;
                SetHaveTiming(true);
                yield return new WaitForSeconds(10f);
            }
        }
    }
    public void SetHaveTiming(bool isbool)
    {
        haveTiming = isbool;
    }
    public IEnumerator CheckTiming()
    {
        float dis = Mathf.Abs(UiManager.Instance.innerNote.transform.localScale.x - UiManager.Instance.outterNote.transform.localScale.x);
        float att = GameManager.Instance.player.Att;
        if (dis < 0.3f)
        {
            att *= 2;
            Debug.Log("Great");
        }
        else if (dis < 0.5f)
        {
            att *= 1.5f;
            Debug.Log("Good");
        }
        else if (dis < 1f)
        {
            att *= 1f;
            Debug.Log("Normal");
        }
        else
        {
            att *= 0.5f;
            Debug.Log("Bad");
        }
        SetHaveTiming(false);
        UiManager.Instance.note.SetActive(false);
        yield return null;
    }
    IEnumerator HitCool()
    {
        isHit = true;
        yield return new WaitForSeconds(0.5f);
        isHit = false;
    }

    public void Dead(bool force)
    {
        isDead = true;
        if (passiveCor != null)
        {
            StopCoroutine(passiveCor);
            passiveCor = null;
        }
        GameManager.Instance.player.playerStat.AddMoney(bossStat.money);
        GameManager.Instance.player.playerStat.AddExp(bossStat.experience);
        agent.isStopped = true;
        //GameManager.Instance.boss.StopWeak();
        gameObject.SetActive(false);
        GameManager.Instance.killMonster++;

        Debug.Log("����");
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void LevelUp()
    {
        if (bossStat != null)
        {
            bossStat.LevelUp(); // ���� 1�� �ø��� �Լ�
            StatUp();
        }
    }

    public void StatUp()
    {
        bossStat.SetMaxHealth((bossStat.level * 1000) + soOriginBoss.maxHealth);
        bossStat.SetHealth(bossStat.maxHealth);
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
