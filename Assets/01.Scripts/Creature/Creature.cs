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
        bool isCritical = Random.Range(0f, 100f) < Stat.critical;
        return isCritical;
    }
    public float CriticalDamage(float att)
    {
        float criticalDamage = att;
        if (CheckCritical())
        {
            criticalDamage = att * 1.5f;
        }

        return criticalDamage;
    }
    public virtual void Attack()
    {
        Collider[] colliders = GetAttackRange();
        if (colliders.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                colliders[i].GetComponent<Player>().TakeDamage(Stat.attack);
            }
            else if (colliders[i].CompareTag("Monster"))
            {
                colliders[i].GetComponent<Monster>().TakeDamage(Stat.attack);
            }
            else if (colliders[i].CompareTag("Boss"))
            {
                colliders[i].GetComponent<Boss>().TakeDamage(Stat.attack);
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
    public abstract void TakeDamage(float att);
    #endregion


    #region 스킬

    #endregion


    #region 죽음
    public abstract void Die();
    public void SetIsDead(bool on)
    {
        isDead = on;
    }
    #endregion
}
