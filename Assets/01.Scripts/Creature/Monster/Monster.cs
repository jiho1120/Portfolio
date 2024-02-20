using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Monster : Creature, ReInitialize
{
    public AllEnum.MonsterType monType;
    public AllEnum.States NowState = AllEnum.States.End;//�������    
    MonsterAnimation anim; //��� ��¥ �ܼ��� �ִϸ��̼� ���...    
    NavMeshAgent agent;
    public NavMeshAgent Agent => agent;
    public MONStateMachine monStateMachine { get; private set; }
    public SOMonster soOriginMonster;
    public MonsterStat monsterStat { get; private set; } // �ٲ�� ����
    public Vector3 dir;
    public GameObject explosionEffect;
    Rigidbody rb;

    public Transform attackPos;
    public bool isAttack = false;  // ���� ��Ÿ���� �༭ �ð��� �Ǹ� Ʈ��� �ٲٰ�
    public bool isHit = false;
    public float coolAttackTime = 0;
    public float rotationSpeed = 5f;
    protected int itemIndex;
    Vector3 itempos = new Vector3(0, 1, 0);

    public Coroutine dieCor = null;
    public bool force { get; private set; }

    #region ReInitialize
    public override void Init()
    {
        anim = GetComponent<MonsterAnimation>();
        anim.SetInit();
        agent = GetComponent<NavMeshAgent>();
        monStateMachine = GetComponent<MONStateMachine>();
        monStateMachine.SetInit();
        monsterStat = new MonsterStat();
        monsterStat.SetValues(soOriginMonster);
        rb = GetComponent<Rigidbody>();
        dieCor = null;
        isAttack = false;
        isHit = false;
        isDead = false;
        force = false;
        dieCor = null;
    }

    public override void ReInit()
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
            monStateMachine = GetComponent<MONStateMachine>();
            monStateMachine.SetInit();
        }

        if (monsterStat == null)
        {
            monsterStat = new MonsterStat();
            monsterStat.SetValues(soOriginMonster);
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (dieCor != null)
        {
            StopCoroutine(dieCor);
            dieCor = null;
        }
        StartCoroutine(StopKnockBack());
        isAttack = false;
        isHit = false;
        isDead = false;
        force = false;
        agent.baseOffset = 0f; // �߷����� �����ְ� �ڷ�ƾ ���������� �¾��� ��ġ �ʱ�ȭ�� �ȵǼ� �����Ҷ� ����
        monsterStat.SetLevel(GameManager.Instance.countGame);
        StatUp();//�� ������ �´� ������ ������
        monStateMachine.SetState(AllEnum.States.Idle);
        dieCor = null;
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    public void SetStopAndMove()
    {
        StartCoroutine(StartMoveTimer());
    }
    public IEnumerator StopKnockBack()
    {
        agent.isStopped = true;
        rb.isKinematic = true;
        yield return new WaitForSeconds(0.1f);
        rb.isKinematic = false;
        agent.isStopped = false;
    }
    public IEnumerator StartMoveTimer()
    {
        if (rb != null)
        {
            agent.isStopped = true;
            yield return new WaitForSeconds(1f);
            rb.isKinematic = true;
            yield return new WaitForSeconds(1f);
            rb.isKinematic = false;
            agent.isStopped = false;
        }
    }

    public Vector3 CheckDir()
    {
        dir = GameManager.Instance.player.transform.position - this.transform.position;
        dir.y = 0;

        return dir;
    }

    public override void TakeDamage(float critical, float attack)
    {
        if (isDead)
        {
            return;
        }
        isHit = true;
        float damage = CriticalDamage(critical, attack) - (monsterStat.defense * 0.5f); // ���� ���� �߰�
        float hp = monsterStat.health - damage;
        monsterStat.SetHealth(hp);
        if (monsterStat.health <= 0)
        {
            monsterStat.SetHealth(0);
            isDead = true;
        }
    }
    public override void Attack(Vector3 Tr, float Range)
    {
        Collider[] colliders = Physics.OverlapSphere(Tr, Range);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                colliders[i].GetComponent<Player>().TakeDamage(monsterStat.criticalChance, monsterStat.attack);
            }
        }
    }
    public void AttackRange() // �ִϸ��̼ǿ� ����
    {
        Attack(attackPos.position, 0.5f);
    }

    public void Explosion()
    {
        Attack(transform.position, 2f);
        Instantiate(explosionEffect, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
    }

    //dead�� �Ҹ��� ���
    //1 �ʵ忡 ������ ����־��� //Ȱ��ȭ �Ǿ��ְ� �׾���ǥ�� �ȵǾ�����.
    //2 �ʵ忡 �־����� �ױ� ������̾��� //Ȱ��ȭ �Ǿ�������, �׾��� ǥ��Ǿ��ִ� ����
    //3 �ʵ忡 ������. => ��Ȱ��ȭ ����
    public override void Dead(bool _force)
    {
        isDead = true;
        force = _force;
    }


    IEnumerator DeletObject()
    {
        yield return new WaitForSeconds(2f);
        if (monType == AllEnum.MonsterType.Explosion)
        {
            Explosion();
            yield return new WaitForSeconds(0.1f);
        }

        MonsterManager.Instance.MonsterPool().ReturnObjectToPool(this);
        dieCor = null;
    }

    public void DropRandomItem()
    {
        itemIndex = Random.Range(0, 3);
        if (itemIndex == 0)
        {
            GameManager.Instance.player.playerStat.AddMoney(monsterStat.money);
        }
        else
        {
            if (itemIndex == 1) // ���
            {
                itemIndex = Random.Range(0, 7);
            }
            else if (itemIndex == 2) // ����
            {
                itemIndex = Random.Range(101, 104);

            }
            ItemManager.Instance.DropItem(itemIndex, transform.position + itempos);
        }
    }

    #region level
    public override void LevelUp() // �ʿ������
    {
        monsterStat.LevelUp();
    }

    public override void StatUp()
    {
        monsterStat.SetMaxHealth((monsterStat.level * 10) + soOriginMonster.maxHealth);
        monsterStat.SetHealth(soOriginMonster.maxHealth);
        monsterStat.SetAttack((monsterStat.level * 30) + soOriginMonster.attack);
        monsterStat.SetDefence((monsterStat.level * 10) + soOriginMonster.defense);
        monsterStat.SetcriticalChance((monsterStat.level * 0.5f) + soOriginMonster.criticalChance);
        monsterStat.SetSpeed((monsterStat.level * 0.1f) + soOriginMonster.movementSpeed);
        monsterStat.SetExp((monsterStat.level * 10) + soOriginMonster.experience);
        monsterStat.SetMoney((monsterStat.level * 10) + soOriginMonster.money);
    }
    #endregion
    #region  State ���� ����
    public void Idle()
    {
        if (agent != null)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            SetIdelAnim();
        }
        
    }

    public void Move(Vector3 vec)
    {
        agent.isStopped = false;
        agent.SetDestination(vec);
        SetMoveAnim();
    }
    public void Attack()
    {
        agent.isStopped = true;
        SetAttackAnim();
    }
    public void SetAttackState()
    {
        coolAttackTime += Time.deltaTime;
        if (coolAttackTime >= 5f)
        {
            coolAttackTime = 0;
            isAttack = true;
        }
    }

    public void Hit()
    {
        SetHitAnim();
        agent.isStopped = true;
    }

    public void StartDieCor()
    {
        dieCor = StartCoroutine(DeletObject());

    }
    public void StopDieCor()
    {
        if (dieCor != null)
        {
            StopCoroutine(dieCor);
            dieCor = null;
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
        anim.Attack();
    }
    public void SetHitAnim()
    {
        anim.Hit();
    }
    public void SetDeadAnim()
    {
        if (agent != null)
        {
            try
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                if (anim != null)
                    anim.Die();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        //anim.Die();
    }

    #endregion
}
