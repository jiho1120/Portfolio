using UnityEngine;
using static AllEnum;

public class PassiveSkill : Skill
{
    public PassiveSkill(SkillData skillData, ObjectType caster) : base(skillData, caster)
    {
    }

    public override void ApplyEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void RemoveEffect()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
