using UnityEngine;

public class AirCircle : ActiveSkill, IKnockback
{
    private Collider[] colliders;
    public float radius = 5f;
    float knockbackPower = 5f;
    public override void Activate()
    {
        base.Activate();
        DetectEnemies();

        for (int i = 0; i < colliders.Length; i++)
        {
            Creature cre = colliders[i].GetComponent<Creature>();

            cre.TakeDamage(skilldata.effect);
            SetKnockbackCondition(cre);
        }
    }

    public void SetKnockbackCondition(Creature cre)
    {
        Vector3 vec = (cre.transform.position - transform.position).normalized;
        vec.y = 0; // 수평 방향으로만 넉백

        cre.KnockbackDirection = vec;
        cre.KnockbackPower = knockbackPower;
        cre.IsKnockback = true;
        if (cre is Player)
        {
            cre.KnockbackPower = 10;
            cre.Knockback();
        }
    }

    private void DetectEnemies()
    {
        colliders = Physics.OverlapSphere(transform.position, radius, enemyLayer);
    }

  


}
