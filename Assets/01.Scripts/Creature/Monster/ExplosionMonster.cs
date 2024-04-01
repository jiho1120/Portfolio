using UnityEngine;

public class ExplosionMonster : Monster
{
    public GameObject ExplosionEffect;

    public override void Die()
    {
        ExplosionEffect.gameObject.SetActive(true);
        AttackRange(transform.position, 2f);
        base.Die();
    }
    
}
