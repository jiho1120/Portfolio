using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        anim = GetComponent<MonsterAnimation>();
        anim.SetInit();
        agent = GetComponent<NavMeshAgent>();
        monsterStat = new MonsterStat();
        monStateMachine = GetComponent<MONStateMachine>();
        monStateMachine.SetInit();
        monsterStat.SetValues(soOriginMonster); // �����ö��� ����  ������ ���� �÷��ִ�  �Լ� ���� ���� �÷��ֱ� json ���Ϸ� �����ϱ�  
        monsterStat.ShowInfo();
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
        //float damage = CriticalDamage(critical, attack) - (defense * 0.5f); // ���� ���� �߰�
        //health -= damage;
        //if (health < 0)
        //{
        //    health = 0;
        //}
        Debug.Log("���� ����");

    }


    public void Idle()
    {
        anim.Idle();
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    public void Move(Vector3 vec, bool anim = false)
    {
        if (anim)
        {
            SetMoveAnim();
        }
        agent.isStopped = false;
        agent.SetDestination(vec);
        //���ǿ� ����
        //Walk�� �ִϸ��̼��� �������� �� �ÿ�ġ ���� ���� ����..
        //������ SetMoveAnim���� ���� �и���.        
    }
    public void SetMoveAnim()
    {
        anim.Walk(true);
    }

    public virtual void Dead()
    {
        Debug.Log("����");
    }
}
