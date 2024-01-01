using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IAttack
{
    public AllEnum.States NowState = AllEnum.States.End;//�������
    MonsterAnimation anim; //��� ��¥ �ܼ��� �ִϸ��̼� ���...
    NavMeshAgent agent;
    MONStateMachine monStateMachine;
    public SOMonster soOriginMonster;
    MonsterStat monsterStat;
    public Vector3 dir;
    public Transform TargetTr {  get; private set; }

    public bool isAttack = false;  // ���� ��Ÿ���� �༭ �ð��� �Ǹ� Ʈ��� �ٲٰ�
    public bool isHit = false;
    public bool isDead = false;
    public float coolAttackTime = 0;


    private void Start()
    {
        anim = GetComponent<MonsterAnimation>();
        anim.SetInit();
        agent = GetComponent<NavMeshAgent>();
        monStateMachine = GetComponent<MONStateMachine>();
        monStateMachine.SetInit();
        monsterStat = new MonsterStat();
        monsterStat.SetValues(soOriginMonster); // �����ö��� ����  ������ ���� �÷��ִ�  �Լ� ���� ���� �÷��ֱ� json ���Ϸ� �����ϱ�  
        //monsterStat.ShowInfo();
        isAttack = true;

    }

    public void SetTarget(Transform tr)
    {
        this.TargetTr = tr;
    }
    public Vector3 CheckDir()
    {
        dir = TargetTr.position - this.transform.position;
        return dir;
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
            Debug.Log("ũ�� ��");
        }
        else
        {
            criticalDamage = attack;
            Debug.Log("ũ�� �� ��");

        }

        return criticalDamage;
    }
    public virtual void Hit(float critical, float attack)
    {
        TakeDamage(critical, attack);
    }
    /// <summary>
    ///  ������ ����(�⺻���ݸ� ������)
    /// </summary>
    /// <param name="critical">���� ũ��Ƽ�� Ȯ��</param>
    /// <param name="attack">���� ���ݷ�</param>

    public virtual void TakeDamage(float critical, float attack)
    {
        float damage = CriticalDamage(critical, attack) - (this.monsterStat.defense * 0.5f); // ���� ���� �߰�
        float hp = this.monsterStat.health - damage;
        monsterStat.SetHealth(hp);
        if (this.monsterStat.health < 0)
        {
            monsterStat.SetHealth(0);
            isDead = true;
        }
        Debug.Log($"{monsterStat.health}");
    }

    public void Idle()
    {
        SetIdelAnim();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
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
    public virtual void Dead()
    {
        SetDeadAnim();
        Debug.Log("����");
        Invoke("DeletObject",3f);
    }
    public void DeletObject()
    {
        GameManager.Instance.monsterPool.ReturnObjectToPool(this);
    }
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
        anim.Die();
    }


}
