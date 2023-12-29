using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IAttack
{
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

    }

    public virtual void Dead()
    {
        //Destroy(this.gameObject.transform.GetChild(0).gameObject); ���ľ���
        Debug.Log("����");
    }
}
