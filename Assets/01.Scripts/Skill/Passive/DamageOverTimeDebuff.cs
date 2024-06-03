using System.Collections;
using UnityEngine;
using static AllEnum;

// ��Ʈ �����
public class DamageOverTimeDebuff : PassiveSkill
{
    private float damagePerSecond;
    private Coroutine damageCoroutine;

    public DamageOverTimeDebuff(SkillData skillData, ObjectType caster) : base(skillData, caster)
    {
    }

    public override void ApplyEffect()
    {
        // �ֱ������� ���ظ� ������ �ڷ�ƾ ����
        damageCoroutine = StartCoroutine(InflictDamageOverTime());
        Debug.Log("DoT debuff applied: " + damagePerSecond + " damage per second");
    }

    public override void RemoveEffect()
    {
        // ����� ���� �� ���� �ڷ�ƾ �ߴ�
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }
        Debug.Log("DoT debuff removed");
    }

    private IEnumerator InflictDamageOverTime()
    {
        while (data.duration > 0)
        {
            // ���ظ� ������ �κ�
            DealDamageToTarget(damagePerSecond);

            // �ֱ������� ���ظ� ������ ����(��: 1��)�� ��ٸ��ϴ�.
            yield return new WaitForSeconds(1f);

            data.duration -= 1f;
        }
    }

    private void DealDamageToTarget(float damage)
    {
        // ������ ���ظ� ������ �κ�
        // �� ���ÿ����� Debug.Log�� ����Ͽ�����, �����δ� ��󿡰� ���ظ� ������ ��
        Debug.Log("Dealing " + damage + " damage to the target");
    }
}
