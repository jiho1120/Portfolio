using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkill : Node
{
    Boss owner;
    

public UseSkill(Boss owner)
    {
        this.owner = owner;
    }
    public override AllEnum.NodeState Evaluate()
    {
        if (owner.useableSKill)
        {
            if (owner.CheckDistance() <= 49) // 스킬 사거리
            {
                if (owner.skillcor == null)
                {
                    owner.StartSkillTime();
                }
                owner.NowState = AllEnum.StateEnum.Skill;
                return AllEnum.NodeState.Success;
            }
            else
            {
                return AllEnum.NodeState.Failure;
            }
        }
        else
        {
            return AllEnum.NodeState.Failure;
        }
    }

    
}
