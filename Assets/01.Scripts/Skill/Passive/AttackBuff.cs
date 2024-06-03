using UnityEngine;
using static AllEnum;

// ���ݷ� ���� ����
public class AttackBuff : PassiveSkill
{
    private float attackIncrease;

    public AttackBuff(SkillData skillData, ObjectType caster) : base(skillData, caster)
    {
    }

    public override void ApplyEffect()
    {
        // ���ݷ��� ������Ŵ
        // ���÷� Debug.Log�� ����Ͽ�����, ������ ���ݷ��� �������Ѿ� ��
        Debug.Log("Attack buff applied: +" + attackIncrease + " Attack");
    }

    public override void RemoveEffect()
    {
        // ���÷� Debug.Log�� ����Ͽ�����, ������ ���ݷ��� �����ؾ� ��
        Debug.Log("Attack buff removed: -" + attackIncrease + " Attack");
    }
}