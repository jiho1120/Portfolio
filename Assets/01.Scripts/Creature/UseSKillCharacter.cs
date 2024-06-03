using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSKillCharacter : Creature
{
    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    public override void Activate()
    {
        throw new System.NotImplementedException();
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(float att)
    {
        throw new System.NotImplementedException();
    }


    #region 패시브 스킬
    private List<PassiveSkill> passiveSkills = new List<PassiveSkill>();

    public void ApplyBuff(PassiveSkill effect)
    {
        effect.ApplyEffect();
        passiveSkills.Add(effect);
        StartCoroutine(RemoveEffectAfterDuration(effect));
    }

    private IEnumerator RemoveEffectAfterDuration(PassiveSkill effect)
    {
        yield return new WaitForSeconds(effect.GetDuration());
        effect.RemoveEffect();
        passiveSkills.Remove(effect);
    }

    public void RemoveAllEffects()
    {
        foreach (PassiveSkill effect in passiveSkills)
        {
            effect.RemoveEffect();
        }
        passiveSkills.Clear();
    }
    #endregion
}
