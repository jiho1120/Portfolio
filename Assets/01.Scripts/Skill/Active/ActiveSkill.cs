using System.Collections;
using UnityEngine;

// �����ϴ� ȸ���ⰰ�� ��ų ������ Ŭ���� ������ �����ϱ� 
// ���� ��ų�� �� ���̶� IDamage����� ����
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

    // ��ų ���ӽð���ŭ �����ϴٰ� ����
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
