using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour, IAttack, IDead, ILevelUp
{
    public SOBoss soOriginBoss;
    public BossStat bossStat { get; private set; }
    PlayerAnimator animator; // �÷��̾�� �Ȱ���
    public NavMeshAgent agent { get; private set; }
    public Transform characterBody;
    public Transform fist;
    private float attackSpeed = 1;
    private float lastClickTime = 0f;
    private float attackCooldown = 1.5f;
    public float speed { get; private set; }
    bool isLeft = false;
    bool isDead = false;
    Coroutine HealHpMpCor;
    public void FirstStart()
    {
        Init();
        animator = GetComponent<PlayerAnimator>();
        agent = GetComponent<NavMeshAgent>();
        fist = transform.GetChild(0).GetChild(3);
        animator.Starts();
        animator.SetAttackSpeed(attackSpeed);

        //HealHpMpCor = StartCoroutine(HealHpMp());
    }

    public void Init()
    {
        bossStat = new BossStat(soOriginBoss);
    }

    void BasicAttack()
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
            float damage = Mathf.Max(CriticalDamage(critical, attack) - (bossStat.defense * 0.5f), 1f); // �ּ� ������ 1
            float hp = bossStat.health - damage;
            bossStat.SetHealth(hp);
            if (hp < 0)
            {
                bossStat.SetHealth(0);
                Dead(false);
            }
        }
        else
        {
            Debug.Log("�̹� �׾���");
        }
    }
    public void Dead(bool force)
    {
        isDead = true;
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
    public void SetMoveAnim(float z, float x)
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
