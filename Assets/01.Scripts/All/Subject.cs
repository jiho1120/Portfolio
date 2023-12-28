using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    public void ChangeHealth(float health, float damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
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
            Debug.Log("ũ�� ��");
        }
        else
        {
            criticalDamage = attack;
            Debug.Log("ũ�� �� ��");

        }

        return criticalDamage;
    }

    public virtual void Hit(float critical, float attack, float health, float defense)
    {
        TakeDamage(critical, attack, health, defense);
    }

    /// <summary>
    ///  ������ ����(�⺻���ݸ� ������)
    /// </summary>
    /// <param name="critical">���� ũ��Ƽ�� Ȯ��</param>
    /// <param name="attack">���� ���ݷ�</param>
    /// <param name="health">���� ��</param>
    /// <param name="defense">���� ����</param>
    public virtual void TakeDamage(float critical, float attack, float health, float defense)
    {
        float damage = CriticalDamage(critical, attack) - (defense * 0.5f);
        ChangeHealth(health , damage);

    }

    public virtual void Dead()
    {
        //Destroy(this.gameObject.transform.GetChild(0).gameObject); ���ľ���
        Debug.Log("����");
    }
}
