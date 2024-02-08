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
    public void Initialize(); // 처음 설정할것

    public void ReStart(); // 재사용할때 

    public void Deactivate(); //비활성화 할때

    public void DontUse(); // 안쓰게 될때
    
}




