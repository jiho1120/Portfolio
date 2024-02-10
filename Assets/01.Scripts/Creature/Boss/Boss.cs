using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Creature
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
    public bool useableSKill = true;
    public bool isStop;
    public int bossLayer;
    public int PassiveCurrentNum;
    public Coroutine skillcor = null;
    public Coroutine passiveCor { get; private set; }
    Coroutine weakCor = null;

    public bool isHit { get; private set; }
    public bool haveTiming { get; private set; }

    public float distance { get; private set; }

    #region ReInitialize
    public override void Init()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<PlayerAnimator>();
        agent = GetComponent<NavMeshAgent>();
        fist = transform.GetChild(0).GetChild(3);
        animator.Starts();
        animator.SetAttackSpeed(attackSpeed);
        bossStat = new BossStat(soOriginBoss);
        bossLayer = 1 << LayerMask.NameToLayer("Player");
        PassiveCurrentNum = Random.Range((int)AllEnum.SkillName.Fire, (int)AllEnum.SkillName.End); // �ʹ� �ѹ� ���� �̰͵� ������
        isDead = false;
        rb.isKinematic = false;
        SetHaveTiming(false);

    }

    public override void ReInit()
    {
        gameObject.SetActive(true);
        agent.isStopped = false;
        isDead = false;
        rb.isKinematic = false;
        gameObject.transform.position = GameManager.Instance.player.transform.position + new Vector3(3, 0, 3);
        gameObject.transform.LookAt(GameManager.Instance.player.transform.position);
        LevelUp(); // �ɷ�ġ ����
        SetHaveTiming(false);

        DoPassive();
        agent.isStopped = false;
        GetComponent<BehaviorTree>().SetInit();
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    public void CorReset()
    {
        StopAllCoroutines();
        skillcor = null;
        passiveCor = null;
        weakCor = null;
    }
   
    public void DoPassive()
    {
        if (passiveCor == null)
        {
            passiveCor = StartCoroutine(DoPassive(true));
        }
    }
    IEnumerator DoPassive(bool isPlayer)
    {
        while (GameManager.Instance.stageStart)
        {
            PassiveSkill ps = SkillManager.Instance.CallPassiveSkill(isPlayer);
            yield return new WaitForSeconds(ps.skillStat.duration);
        }
    }
    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        SetMoveAnim(0, agent.velocity.z, agent.velocity.x);
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
    public override void Attack(Vector3 Tr, float Range)
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

    
    public override void TakeDamage(float critical, float attack)
    {
        if (!isDead)
        {
            if (!isHit)
            {
                if (haveTiming)
                {
                    CheckTiming();
                    StartCoroutine(HitCool());
                }
                Hit();
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

    public void StartWeakCor()
    {
        if (weakCor == null)
        {
            weakCor = StartCoroutine(StartWeak());
        }
        else
        {
            return;
        }
    }
    public void StopWeakCor()
    {
        if (weakCor != null)
        {
            StopCoroutine(weakCor);
            weakCor = null;
            UiManager.Instance.StopShrinke();
        }
        else
        {
            return;
        }
    }
    public IEnumerator StartWeak()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            SetHaveTiming(true);
            UiManager.Instance.StartShrinke();
            yield return new WaitForSeconds(5f);
        }
    }
    public void SetHaveTiming(bool isbool)
    {
        haveTiming = isbool;
    }
    public void CheckTiming()
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
        UiManager.Instance.StopShrinke();
        UiManager.Instance.note.SetActive(false);
        
    }
    IEnumerator HitCool()
    {
        isHit = true;
        yield return new WaitForSeconds(0.5f);
        isHit = false;
    }

    public override void Dead(bool force)
    {
        isDead = true;
        if (passiveCor != null)
        {
            StopCoroutine(passiveCor);
            passiveCor = null;
        }
    }

   

    public override void LevelUp()
    {
        if (bossStat != null)
        {
            bossStat.LevelUp(); // ���� 1�� �ø��� �Լ�
            StatUp();
        }
    }

    public override void StatUp()
    {
        bossStat.SetMaxHealth((bossStat.level * 1000) + soOriginBoss.maxHealth);
        bossStat.SetHealth(bossStat.maxHealth);
        bossStat.SetAttack((bossStat.level * 10) + soOriginBoss.attack);
        bossStat.SetDefence((bossStat.level * 10) + soOriginBoss.defense);
        bossStat.SetcriticalChance((bossStat.level * 0.5f) + soOriginBoss.criticalChance);
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
