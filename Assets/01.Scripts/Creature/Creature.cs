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
                Debug.Log("�ƹ��� ����");
            }
        }
    }
    public Collider[] GetAttackRange()
    {
        return Physics.OverlapSphere(attackPos.position, AttackRange, EnemyLayerMask);
    }
    public abstract void TakeDamage(float att);
    #endregion


    #region ��ų

    #endregion


    #region ����
    public abstract void Die();
    public void SetIsDead(bool on)
    {
        isDead = on;
    }
    #endregion
}
