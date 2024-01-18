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
        if (owner.CheckDistance() < 16)
        {

            return AllEnum.NodeState.Success;

        }
        else
        {
            return AllEnum.NodeState.Failure;
        }
    }

}
