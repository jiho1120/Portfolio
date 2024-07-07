using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : Node
{
    public Knockback(Boss owner)
    {
        this.owner = owner;
    }
    public override AllEnum.NodeState Evaluate()
    {
        if (owner.IsKnockback)
        {
            owner.Knockback();
            owner.NowState = AllEnum.StateEnum.Knockback;
            return AllEnum.NodeState.Success;
        }
        else
        {
            return AllEnum.NodeState.Failure;
        }

    }
}
