using UnityEngine;

public interface IAttack
{
    public bool CheckCritical(float critical);
    public float CriticalDamage(float critical, float attack);
    public abstract void Attack(Vector3 Tr, float Range);
    public abstract void TakeDamage(float critical, float attack);

}

public interface IDead
{
    public void Dead(bool force);
    public bool IsDead();
}

public interface ILevelUp
{
    public void LevelUp();
    public void StatUp();
}

public interface ReInitialize
{
    public void Initialize(); // ó�� �����Ұ�

    public void ReStart(); // �����Ҷ� 

    public void Deactivate(); //��Ȱ��ȭ �Ҷ�

    public void DontUse(); // �Ⱦ��� �ɶ�
    
}




