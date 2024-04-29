using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Creature : MonoBehaviour, Initialize
{
    public Transform attackPos;
    protected float AttackRange;
    protected int EnemyLayerMask;
    public StatData Stat { get; protected set; }
    public bool isDead { get; protected set; } = false;


    #region �ʱ�ȭ Ȱ����Ȱ��
    public abstract void Init();
    public abstract void Activate();

    public abstract void Deactivate();
    #endregion



    #region ���� ����
    public void LevelUp()
    {

    }
    public void StatUp()
    {

    }
    #endregion

    #region ����
    public bool CheckCritical()
    {
        Debug.Log(Stat.critical);
        bool isCritical = Random.Range(0f, 100f) < Stat.critical;
        return isCritical;
    }
    public float CriticalDamage()
    {
        float criticalDamage;
        if (CheckCritical())
        {
            Debug.Log(Stat.attack);
            criticalDamage = Stat.attack * 1.5f;
            Debug.Log("ġ��Ÿ ����");
        }
        else
        {
            criticalDamage = Stat.attack;
            Debug.Log("ġ��Ÿ �� ����");

        }

        return criticalDamage;
    }
    public virtual void Attack()
    {
        Collider[] colliders = GetAttackRange();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                colliders[i].GetComponent<Player>().TakeDamage();
            }
            else if (colliders[i].CompareTag("Monster"))
            {
                colliders[i].GetComponent<Monster>().TakeDamage();
            }
            else if (colliders[i].CompareTag("Boss"))
            {
                colliders[i].GetComponent<Boss>().TakeDamage();
            }
            
            else
            {
                Debug.Log("�ƹ��� ����");
            }
        }
    }
    public Collider[] GetAttackRange()
    {
        return Physics.OverlapSphere(attackPos.position, AttackRange, EnemyLayerMask);
    }
    public abstract void TakeDamage();
    #endregion


    #region

    #endregion


    #region ����
    public abstract void Die();
    public void SetIsDead(bool on)
    {
        isDead = on;
    }
    #endregion










}
