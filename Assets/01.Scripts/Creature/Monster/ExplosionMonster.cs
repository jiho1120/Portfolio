using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionMonster : Monster
{
    public GameObject ExplosionEffect;


    public override void Die()
    {
        base.Die();
        ExplosionEffect.gameObject.SetActive(true);
        AttackRange(transform.position, 2f);
    }
}
