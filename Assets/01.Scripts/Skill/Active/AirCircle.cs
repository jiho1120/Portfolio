using System.Runtime.ConstrainedExecution;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class AirCircle : NonTerrainEffectSkill, IKnockback, IHit
{
    float power = 10f;

    public void SetKnockbackCondition(Creature cre)
    {
        Vector3 vec = (cre.transform.position - transform.position).normalized;
        vec.y = 0; // 수평 방향으로만 넉백

        cre.KnockbackDirection = vec;
        cre.KnockbackPower = power;
        cre.IsKnockback = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Creature cre =  other.GetComponent<Creature>();
        if (cre != null)
        {
            if (cre.IsKnockback == true)
            {
                return;
            }
            Debug.Log("컨디션함수 실행");
            SetKnockbackCondition(cre);
        }
    }

    
}
