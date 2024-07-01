using UnityEngine;
using static AllEnum;
public class UseSKillCharacter : Creature
{
    protected Coroutine passiveCor = null;
    protected SkillName nowPassiveSKillName;
    public override void Activate()
    {
        base.Activate();
        if (passiveCor == null)
        {
            passiveCor = StartCoroutine(SkillManager.Instance.StartPassiveCor(this));
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        SkillManager.Instance.AllSKillDeactive(this);
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void GetAttToData()
    {
        throw new System.NotImplementedException();
    }

    public override void implementTakeDamage()
    {
        throw new System.NotImplementedException();
    }

    


}