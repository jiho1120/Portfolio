using System.Collections;
using UnityEngine;

// 시전하는 회복기같은 스킬 나오면 클래스 나눠서 관리하기 
// 지금 스킬이 다 딜이라 IDamage여기다 붙임
public class ActiveSkill : Skill, IHit
{
    Coroutine activeCor;

    public void Attack(Creature cre)
    {
        cre.GetComponent<Creature>()?.TakeDamage(skilldata.effect);
            Debug.Log(cre.id);
    }
    public override void Activate()
    {
        base.Activate();
        if (activeCor == null)
        {
            activeCor = StartCoroutine(SKillDuration());

        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        if (activeCor != null)
        {
            StopCoroutine(activeCor);
            activeCor = null;
        }
    }

    // 스킬 지속시간만큼 존재하다가 꺼짐
    IEnumerator SKillDuration()
    {
        yield return new WaitForSeconds(skilldata.duration);
        Deactivate();
    }

    protected override void OnTriggerEnter(Collider other)
    {

    }

    protected override void OnCollisionEnter(Collision collision)
    {

    }

    
}
