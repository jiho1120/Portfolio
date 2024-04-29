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


    #region 초기화 활성비활성
    public abstract void Init();
    public abstract void Activate();

    public abstract void Deactivate();
    #endregion



    #region 레벨 관련
    public void LevelUp()
    {

    }
    public void StatUp()
    {

    }
    #endregion

    #region 공격
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
            Debug.Log("치명타 터짐");
        }
        else
        {
            criticalDamage = Stat.attack;
            Debug.Log("치명타 안 터짐");

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
                Debug.Log("아무도 없음");
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


    #region 죽음
    public abstract void Die();
    public void SetIsDead(bool on)
    {
        isDead = on;
    }
    #endregion










}
