using UnityEngine;

public interface IAttack
{
    public bool CheckCritical(float critical);
    public float CriticalDamage(float critical, float attack);
    public void Attack(Transform Tr, float Range);
    public void TakeDamage(float critical, float attack);

    
}

public interface IDead
{
    public void Dead();
    public bool IsDead();
}






    
