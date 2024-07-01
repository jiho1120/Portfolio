using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public interface Initialize
{
    public void Activate(); //Ȱ��ȭ �Ҷ�

    public void Deactivate(); //��Ȱ��ȭ �Ҷ�
}

public interface IProduct
{
    // add common properties and methods here
    public string ProductName { get; set; }

    // customize this for each concrete product
    public void Init();// ó�� �����Ұ�
    
}

public interface IEquipable
{
    void Equip(); // ���� ����
}

public interface IUsable
{
    void Use(); // ��� ����
}

public interface IAttack // ��Ÿ��� ����
{
    void Attack();
    bool CheckCritical();
    float CriticalDamage(float att);
}

// �����̻�ȿ��
public interface IStatusEffect
{
    bool IsKnockback { get; set; }
    bool IsPull { get; set; }
    void Knockback();
    void Pull(Vector3 targetPosition);
    IEnumerator StopForceMove(float seconds);

}

// �нú� ȿ���� �ʿ��� �Լ���
public interface IBuffAndDebuff
{
    void SetAtt(float effect);
    void StartDecreaseAttCor(float effect, float seconds);
    void StopDecreaseAttCor();
    void GetAttToData();

    void StartSetHPCor(float effect, float seconds);
    void StopSetHpCor();
    void TakeDamage(float att);
}

public interface IKnockback
{
    /// <summary>
    /// ��ų�� �̴��� power ���� �����ؾ���, y���� ��Ȳ�� ���� �����ϱ�
    /// </summary>
    /// <param name="cre"></param>
    /// <param name="_forcePower"></param>
    void SetKnockbackCondition(Creature cre);
}

public interface IPull
{
    void SetPullCondition(Creature cre);
}

public interface IHit
{
    void Attack(Creature cre);
}

public interface IHeal
{

}

public interface IDecreaseAtt
{

}

public interface IIncreaseAtt
{

}






