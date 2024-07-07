using System.Collections;
using UnityEngine;

public class Wind : PassiveSkill
{
    private Collider[] colliders;
    float radius = 5f;
    protected float ReTriggerTime = 2f; // �ٽ� �����ϴ½ð�

    Coroutine attCor = null;
    
    public override void Deactivate()
    {
        if (attCor != null)
        {
            StopCoroutine(attCor);
            attCor = null;
        }
        base.Deactivate();
    }
    protected override void ImplementEffects()
    {
        base.ImplementEffects();
        if (attCor == null)
        {
            attCor = StartCoroutine(AttackCor());
        }
    }
    IEnumerator AttackCor()
    {
        while (true)
        {
            DetectEnemies();
            for (int i = 0; i < colliders.Length; i++)
            {
                Creature cre = colliders[i].GetComponent<Creature>();

                cre.TakeDamage(skilldata.effect);
            }
            yield return new WaitForSeconds(ReTriggerTime);
        }
        
    }
    private void DetectEnemies()
    {
        colliders = Physics.OverlapSphere(transform.position, radius, enemyLayer);
    }




}
