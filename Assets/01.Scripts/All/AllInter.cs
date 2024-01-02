public interface IAttack
{
    public bool CheckCritical(float critical);
    public float CriticalDamage(float critical, float attack);
    public void Hit(float critical, float attack);

   
    public void TakeDamage(float critical, float attack);

    public void Dead();

    public bool IsDead();
}





    
