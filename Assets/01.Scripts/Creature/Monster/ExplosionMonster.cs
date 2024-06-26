using UnityEngine;

public class ExplosionMonster : Monster
{
    public GameObject ExplosionEffect;

    public override void Init()
    {
        ProductName = "ExplosionMonster";
        base.Init();
    }
    public override void Die()
    {
        ExplosionEffect.gameObject.SetActive(true);
        GetAttackRange();
        base.Die();
    }

}
