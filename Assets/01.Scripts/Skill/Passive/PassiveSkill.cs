using System.Collections;
using UnityEngine;

public class PassiveSkill : Skill
{
    protected Coroutine PassiveTimeCor;
    public override void Activate()
    {
        base.Activate();
        if (PassiveTimeCor == null)
        {
            PassiveTimeCor = StartCoroutine(PassiveTimer());
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        if (PassiveTimeCor != null)
        {
            StopCoroutine(PassiveTimeCor);
            PassiveTimeCor = null;
        }
        

    }

    protected IEnumerator PassiveTimer()
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
