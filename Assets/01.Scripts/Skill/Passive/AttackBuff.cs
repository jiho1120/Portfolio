using UnityEngine;
using static AllEnum;

// 공격력 증가 버프
public class AttackBuff : PassiveSkill
{
    private float attackIncrease;

    public AttackBuff(SkillData skillData, ObjectType caster) : base(skillData, caster)
    {
    }

    public override void ApplyEffect()
    {
        // 공격력을 증가시킴
        // 예시로 Debug.Log를 사용하였지만, 실제로 공격력을 증가시켜야 함
        Debug.Log("Attack buff applied: +" + attackIncrease + " Attack");
    }

    public override void RemoveEffect()
    {
        // 예시로 Debug.Log를 사용하였지만, 실제로 공격력을 복원해야 함
        Debug.Log("Attack buff removed: -" + attackIncrease + " Attack");
    }
}