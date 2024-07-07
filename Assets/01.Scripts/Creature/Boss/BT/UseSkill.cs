using UnityEngine;
using static AllEnum;


public class UseSkill : Node
{
    public UseSkill(Boss owner)
    {
        this.owner = owner;
    }

    public override NodeState Evaluate()
    {
        owner.LookPlayer();
        // 목표 지점까지의 실제 거리
        owner.ActualDistance = Mathf.Sqrt(owner.CheckDir().sqrMagnitude);

        if (!owner.isAvailableSkill || owner.ActualDistance > 10)
        {
            return NodeState.Failure;
        }
        
        if (SkillManager.Instance.GetSkill(owner, SkillName.AirSlash).GetComponent<ActiveSkill>().CheckUsableSkill(owner))
        {
            SkillManager.Instance.UseSkill(owner, SkillName.AirSlash);
        }
        else if (SkillManager.Instance.GetSkill(owner, SkillName.AirCircle).GetComponent<ActiveSkill>().CheckUsableSkill(owner))
        {
            SkillManager.Instance.UseSkill(owner, SkillName.AirCircle);
        }
        else if (SkillManager.Instance.GetSkill(owner, SkillName.Ground).GetComponent<ActiveSkill>().CheckUsableSkill(owner))
        {
            SkillManager.Instance.UseSkill(owner, SkillName.Ground);
        }
        else
        {
            return NodeState.Failure;
        }
        owner.StartIsAvailableSkillCor();
        owner.NowState = StateEnum.Skill;
        return NodeState.Success;
    }
}
