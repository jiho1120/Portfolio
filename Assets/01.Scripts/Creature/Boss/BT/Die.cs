using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : Node
{
    public Die(Boss owner)
    {
        this.owner = owner;
    }

    public override AllEnum.NodeState Evaluate()
    {
        if (owner.isDead)
        {
           owner.Die();

            owner.NowState = AllEnum.StateEnum.Die;
            return AllEnum.NodeState.Success;
        }
        return AllEnum.NodeState.Failure;
    }
    
}
